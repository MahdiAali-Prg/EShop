using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Common.Services.Image
{
    public static class ImageNameGenerator
    {
        public static string Generate() => $"{Guid.NewGuid()}.png";
    }
}
