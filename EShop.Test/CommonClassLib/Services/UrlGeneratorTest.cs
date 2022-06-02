using System;
using System.Collections.Generic;
using System.Text;
using EShop.Common.Services;
using EShop.Test.Utilities.UrlHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
