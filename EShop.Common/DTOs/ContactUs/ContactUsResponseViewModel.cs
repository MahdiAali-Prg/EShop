using System.ComponentModel.DataAnnotations;

namespace EShop.Common.DTOs.ContactUs
{
    public class ContactUsResponseViewModel
    {
        public long MessageId { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "پیام نمیتواند خالی باشد")]
        public string Response { get; set; }
    }
}
