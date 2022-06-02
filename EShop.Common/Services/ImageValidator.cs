using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EShop.Common.Services
{
    public class ImageValidator
    {
        public static bool IsImageNull(IFormFile image) => image == null;
        public static bool IsImageExtensionPng(IFormFile image) => Path.GetExtension(image.FileName) == ".png";
        public static bool IsImageSizeValid(IFormFile image) => image.Length <= 125_000;
    }
}
