using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Test.Utilities.Repository;
using EShop.Web.Areas.Admin.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Xunit;

namespace EShop.Test.RootProject.Areas.Admin.Components
{
    public class BlogCategoryProcessViewComponentTest
    {
        [Fact]
        public async Task InvokeAsync()
        {
            // Arrange
            BlogCategoryProcessViewComponent viewComponent = new BlogCategoryProcessViewComponent(BlogCategoryRepository.Repository());

            // Act
            var result = await viewComponent.InvokeAsync() as ViewViewComponentResult;

            // Assert
            Assert.Equal("Default", result?.ViewName);
            Assert.Equal(2, (result?.ViewData?.Model as BlogCategoryProcessViewModel)?.CategoriesCount);
        }
    }
}
