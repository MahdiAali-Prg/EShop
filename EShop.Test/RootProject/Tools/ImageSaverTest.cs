using System.IO;
using System.Threading.Tasks;
using EShop.Common.Services.Image;
using EShop.Data.Models;
using EShop.Test.Utilities.Images;
using EShop.Web.Tools;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace EShop.Test.RootProject.Tools
{
    public class ImageSaverTest
    {
        [Fact]
        public async Task Save()
        {
            // Arrange
            string fileDocument = Path.Combine(Directory.GetCurrentDirectory(), "Images", "BrandImage");
            foreach (string file in Directory.GetFiles(fileDocument))
            {
                File.Delete(file);
            }

            IFormFile image = ImageFile.Image();

                // Act

            await image.SaveAsync("BrandImage", new Brand(){Image = ImageNameGenerator.Generate()}, default);

            // Assert
            Assert.Equal(1, Directory.GetFiles(fileDocument)?.Length);



        }
    }
}
