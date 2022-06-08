using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EShop.Web.Tools
{
    public static class ImageSaver
    {
        public static async Task SaveAsync(this IFormFile image, string folderName, object baseClass, CancellationToken cancellationToken = default)
        {
            PropertyInfo imageName = baseClass?.GetType()?.GetProperty("Image");

            if (imageName == null || imageName.PropertyType != typeof(string))
            {
                throw new Exception("System Can't Use This Property");
            }

            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", folderName);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            using (FileStream fileStream = File.Create($"{savePath}/{imageName.GetValue(baseClass, null)}"))
            {
                await image.CopyToAsync(fileStream, cancellationToken);
            }
        }
    }
}
