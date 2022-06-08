using EShop.Common.Services.RouteConstraint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace EShop.Test.CommonClassLib.Services
{
    public class PositivePageIdTest
    {
        [Fact]
        public void Match()
        {
            // Arrange
            PositivePageId positivePageId = new PositivePageId();

            var context = new Mock<HttpContext>();
            // Act
            var result = positivePageId.Match(context.Object, default, "pageId", new RouteValueDictionary()
            {
                {"pageId", (long)0}
            }, RouteDirection.IncomingRequest);

            // Assert
            Assert.False(result);
        }
    }
}
