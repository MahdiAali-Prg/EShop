using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Web.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.TagHelpers
{
    public class PaginationTagHelperTest
    {
        [Fact]
        public void Process()
        {
            // Arrange 
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(s => s.Action(It.IsAny<UrlActionContext>()))
                .Returns("/Controller/Action?pageId=1");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(s => s.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            AdminPaginationTagHelper paginationTagHelper = new AdminPaginationTagHelper(urlHelperFactory.Object)
            {
                Action = "Action",
                Area = "Admin",
                Controller = "Controller",
                PaginationInfo = new PaginationInfo()
                {
                    TotalItems = 3,
                    ItemPerPage = 3,
                    CurrentPage = 1
                }
            };

            TagHelperContext tagHelperContext = new TagHelperContext("", new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();

            TagHelperOutput tagHelperOutput = new TagHelperOutput("div", new TagHelperAttributeList()
                , (cache, encoder) => Task.FromResult(content.Object));

            // Act
            paginationTagHelper.Process(tagHelperContext, tagHelperOutput);

            // Assert
            Assert.Equal(@"<ul class=""pagination""><li class=""active paginate_button page-item""><a class=""page-link"" href=""/Admin/Controller/Action?pageId=1"">1</a></li></ul>", tagHelperOutput.Content.GetContent());


        }
    }
}
