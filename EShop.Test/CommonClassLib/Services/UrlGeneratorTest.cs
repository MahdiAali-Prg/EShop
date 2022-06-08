using EShop.Common.Services.Url;
using EShop.Test.Utilities.UrlHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EShop.Test.CommonClassLib.Services
{
    public class UrlGeneratorTest
    {
        [Fact]
        public void Generate()
        {
            // Arrange
            UrlGenerator generator = new UrlGenerator("Area", "Controller", "Action");

            // Act
            string result = generator?.Generate(UrlHelperFactoryMaker.GetUrlHelperFactory("/Controller/Action"), It.IsAny<ActionContext>());

            // Assert

            Assert.Equal("/Area/Controller/Action", result);
        }
    }
}
