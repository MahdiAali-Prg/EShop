using EShop.Common.Services.Image;
using EShop.Test.Utilities.Images;
using Xunit;

namespace EShop.Test.CommonClassLib.Services
{
    public class ImageValidatorTest
    {
        [Fact]
        public void IsImageNull()
        {
            // Act
            bool result = ImageValidator.IsImageNull(ImageFile.Image());

            // Assert
            Assert.False(result);

        }

        [Fact]
        public void IsImageExtensionPng()
        {
            // Act
            bool result = ImageValidator.IsImageExtensionPng(ImageFile.Image());
            
            // Assert
            Assert.True(result);

        }

        [Fact]
        public void IsImageSizeValid()
        {
            // Act
            bool result = ImageValidator.IsImageSizeValid(ImageFile.Image());

            // Assert
            Assert.True(result);
        }

    }
}
