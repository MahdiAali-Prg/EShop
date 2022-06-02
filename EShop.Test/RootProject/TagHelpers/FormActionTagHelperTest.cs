using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Test.Utilities.UrlHelper;
using EShop.Web.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.TagHelpers
{
    public class FormActionTagHelperTest
    {
        [Fact]
        public void Process()
        {
            // Arrange

            FormActionTagHelper formActionTagHelper = new FormActionTagHelper(UrlHelperFactoryMaker.GetUrlHelperFactory("/Controller/Action"))
            {
                FormAction = "Action",
                FormArea = "Admin",
                FormController = "Controller"
            };

            TagHelperContext context = new TagHelperContext("", new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();

            TagHelperOutput output = new TagHelperOutput("form", new TagHelperAttributeList()
                , (cache, encoder) => Task.FromResult(content?.Object));

            // Act

            formActionTagHelper?.Process(context, output);

            // Assert
            Assert.Equal("/Admin/Controller/Action", output?.Attributes["action"]?.Value?.ToString());
        }
    }
}
