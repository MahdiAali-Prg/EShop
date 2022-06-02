using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Common.Services
{
    public static class ImageNameGenerator
    {
        public static string Generate() => $"{Guid.NewGuid()}.png";
    }
}
