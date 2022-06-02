using EShop.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EShop.Test.RootProject.Areas.Admin.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            HomeController home = new HomeController();

            // Act
            string viewName = (home?.Index() as ViewResult)?.ViewName;

            // Assert
            Assert.Equal("Index", viewName);
        }
    }
}
