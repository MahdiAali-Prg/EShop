using System.ComponentModel.DataAnnotations;

namespace EShop.Data.Models
{
    public class Brand
    {
        [Key]
        public long BrandId { get; set; }

        [Required(ErrorMessage = "نام برند نمیتواند خالی باشد")]
        [Display(Name = "نام برند")]
        [MaxLength(320, ErrorMessage = "نمیتوانید بیشتر از 320 کاراکتر وارد کنید")]
        [MinLength(2, ErrorMessage = "نمیتوانید کمتر از 2 کاراکتر وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage = "توضیحات برند نمیتواند خالی باشد")]
        [Display(Name = "توضیحات برند")]
        [MaxLength(1200, ErrorMessage = "نمیتوانید بیشتر از 1200 کاراکتر وارد کنید")]
        [MinLength(50, ErrorMessage = "توضیحات نمیتواند کمتر از 50 کاراکتر باشد")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "تصویر برند نمیتواند خالی باشد")]
        [Display(Name = "تصویر برند")]
        [MaxLength(70)]
        public string Image { get; set; }

    }
}
