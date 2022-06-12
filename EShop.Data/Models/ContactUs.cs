using System.ComponentModel.DataAnnotations;

namespace EShop.Data.Models
{
    public class ContactUs
    {
        [Key]
        public long ContactUsId { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [MinLength(9, ErrorMessage = "ایمیل نمیتواند کمتر از 9 کاراکتر باشد")]
        [MaxLength(150, ErrorMessage = "ایمیل نمیتواند بیشتر از 150 کاراکتر باشد")]
        [DataType(DataType.EmailAddress, ErrorMessage = "لطفا فرمت صحیح ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا فزمت صحیح ایمیل را وارد کنید")]
        public string Email { get; set; }

        [MinLength(11, ErrorMessage = "شماره تلفن نمیتواند کمتر از 11 کاراکتر باشد")]
        [MaxLength(13, ErrorMessage = "شماره تلفن نمیتواند بیشتر از 13 کاراکتر باشد")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "لطفا فرمت صحیح شماره تلفن را وارد کنید")]
        public string PhoneNumber { get; set; }

         [Required(ErrorMessage = "متن پیام الزامی میباشد")]
         [MinLength(20, ErrorMessage = "متن پیام نمیتواند کمتر از 20 کاراکتر باشد")]
         [MaxLength(800, ErrorMessage = "متن پیام نمیتواند بیشتر از 800 کاراکتر باشد")]
         [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool HasResponse { get; set; } = false;
    }
}
