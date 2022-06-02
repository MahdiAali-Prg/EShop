using System;
using System.Collections.Generic;
using System.Text;
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
    public class RootPaginationTagHelperTest
    {
        [Fact]
        public void Process()
        {
            // Arrange
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(s => s.Action(It.IsAny<UrlActionContext>()))
                .Returns("/Controller/Action?pageId=1")
                .Returns("/Controller/Action?pageId=1")
                .Returns("/Controller/Action?pageId=2")
                .Returns("/Controller/Action?pageId=2");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(s => s.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            TagHelperContext context =
                new TagHelperContext("", new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();

            TagHelperOutput output = new TagHelperOutput("nav", new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            RootPaginationTagHelper paginationTagHelper = new RootPaginationTagHelper(urlHelperFactory.Object)
            {
                RootAction = "Action",
                RootController = "Controller",
                PaginationInfo = new PaginationInfo()
                {
                    CurrentPage = 1,
                    ItemPerPage = 3,
                    TotalItems = 4
                }
            };

            // Act

            paginationTagHelper.Process(context, output);

            // Assert
            Assert.Equal(@"<ul class=""pagination""><li class=""page-item""><a aria-label=""Previous"" class=""page-link"" href=""/Controller/Action?pageId=1""><span aria-hidden=""true"">&#xAB;</span></a></li>"+
                         @"<li class=""page-item""><a class=""active page-link"" href=""/Controller/Action?pageId=1"">1</a></li>" 
                         + @"<li class=""page-item""><a class="" page-link"" href=""/Controller/Action?pageId=2"">2</a></li>"
                         + @"<li class=""page-item""><a aria-label=""Next"" class=""page-link"" href=""/Controller/Action?pageId=2""><span aria-hidden=""true"">&#xBB;</span></a></li></ul>", output.Content.GetContent());
        }
    }
}
