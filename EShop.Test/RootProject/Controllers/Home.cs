using EShop.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EShop.Test.RootProject.Controllers
{
    public class Home
    {
        [Fact]
        public void Index()
        {
            // Arrange
            HomeController home = new HomeController();
            
            // Act
            var result = home?.Index() as ViewResult;

            //Assert
            Assert.Equal("Index", result?.ViewName);
        }
    }
}
