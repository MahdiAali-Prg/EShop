using EShop.Common.Services.Image;
using Xunit;

namespace EShop.Test.CommonClassLib.Services
{
    public class ImageNameGeneratorTest
    {
        [Fact]
        public void Generate()
        {
            // Act
            string imageName = ImageNameGenerator.Generate();

            // Assert
            Assert.True(imageName != null);
        }
    }
}
