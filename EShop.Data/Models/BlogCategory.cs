using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Data.Models
{
    public class BlogCategory
    {
        [Key]
        public long BlogCategoryId { get; set; }

        [Required(ErrorMessage = "لطفا نام دسته بندی را انتخاب کنید")]
        [Remote("NameValidation", "BlogCategory", "Admin",  ErrorMessage = "یک دسته بندی با این نام وجود دارد")]
        [MaxLength(50, ErrorMessage = "نمیتوانید بیشتر از 150 کاراکتر وارد کنید")]
        [MinLength(2, ErrorMessage = "نمیتوانید کمتر از 2 کاراکتر وارد کنید")]
        public string Name { get; set; }

        public long? ParentId { get; set; }

        #region Navigation Property

        public ICollection<BlogCategory> BlogCategories { get; set; }
        public BlogCategory Category { get; set; }

        #endregion

    }
}
