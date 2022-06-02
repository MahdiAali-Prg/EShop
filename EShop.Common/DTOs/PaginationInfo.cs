using System;

namespace EShop.Common.DTOs
{
    public class PaginationInfo
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }

        public int TotalPages
        {
            get => (int)Math.Ceiling((double)TotalItems / ItemPerPage);
        }
    }
}
