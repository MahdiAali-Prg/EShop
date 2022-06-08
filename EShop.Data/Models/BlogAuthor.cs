using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Data.Models
{
    public class BlogAuthor
    {
        [Key]
        public long BlogAuthorId { get; set; }

        [Required(ErrorMessage = "لطفا نام کامل نویسنده را وارد کنید")]
        [MaxLength(50, ErrorMessage = "نام نویسنده نمیتواند بیشتر از 50 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "نام نویسنده نمیتواند کمتر از 3 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "توضیحات کوتاه در مورد نویسنده نمیتواند خالی باشد")]
        [MaxLength(800, ErrorMessage = "توضیحات کوتاه نویتواند بیشتر از 800 کاراکتر باشد")]
        [MinLength(100, ErrorMessage = "توضیحات کوتاه نویسنده نمیتواند کمتر از 100 کاراکتر باشد")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(60)]
        public string Image { get; set; }
        public DateTime RegisterDate { get; set; }

    }
}
