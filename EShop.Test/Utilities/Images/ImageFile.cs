using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EShop.Test.Utilities.Images
{
    internal class ImageFile
    {
        public static readonly object exp = new object();
        public static IFormFile Image()
        {
            lock (exp)
            {
                FileStream file = File.OpenRead(@"D:\OnlineShop\EShop\EShop.Test\Utilities\Images\brand.png");
                IFormFile image = new FormFile(file, 0, file.Length, "Brand", "Brand.png");

                return image;
            }
        }
    }
}
