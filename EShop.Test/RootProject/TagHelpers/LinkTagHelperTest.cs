using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Test.Utilities.UrlHelper;
using EShop.Web.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.TagHelpers
{
    public class LinkTagHelperTest
    {
        [Fact]
        public void Process()
        {
            // Arrange

            LinkTagHelper linkTagHelper = new LinkTagHelper(UrlHelperFactoryMaker.GetUrlHelperFactory("/Controller/Action?id=1"))
            {
                Action = "Index",
                Controller = "Controller",
                Values = new Dictionary<string, object>()
                {
                    {"id", 1}
                }
            };

            TagHelperContext context =
                new TagHelperContext("", new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();

            TagHelperOutput output = new TagHelperOutput("a", new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content?.Object));

            // Act
            linkTagHelper?.Process(context, output);

            // Assert
            Assert.Equal("/Controller/Action?id=1", output.Attributes["href"].Value.ToString());
        }
    }
}
