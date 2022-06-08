using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Common.DTOs
{
    public class BlogCategoryProcessViewModel
    {
        public long ParentCategories { get; set; }
        public long ChildCategories { get; set; }
        public long CategoriesCount { get; set; }

    }
}
