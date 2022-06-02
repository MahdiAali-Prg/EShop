using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EShop.Web.Tools
{
    public static class ImageSaver
    {
        public static async Task SaveAsync(this IFormFile image, string imageName, CancellationToken cancellationToken = default)
        {
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "BrandImage");
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            using (FileStream fileStream = File.Create($"{savePath}/{imageName}"))
            {
                await image.CopyToAsync(fileStream, cancellationToken);
            }
        }
    }
}
