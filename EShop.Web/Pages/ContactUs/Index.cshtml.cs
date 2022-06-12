using System.Threading;
using System.Threading.Tasks;
using EShop.Common.Services.GoogleRecaptcha;
using EShop.Data.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Model = EShop.Data.Models;

namespace EShop.Web.Pages.ContactUs
{
    public class IndexModel : PageModel
    {
        private IConfiguration _configuration;
        private IGenericRepository<Model.ContactUs> _repository;

        public IndexModel(IConfiguration configuration, IGenericRepository<Model.ContactUs> repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public Model::ContactUs ContactUs { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromForm][Bind("Email,PhoneNumber,Message")]Model.ContactUs contactUs, CancellationToken cancellationToken)
        {
            if (await RecaptchaValidator.IsValid(_configuration["SecretKey"], Request.Form["g-recaptcha-response"]))
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddAndSaveAsync(contactUs, cancellationToken);
                    return Redirect("/ContactUs?success=true");
                }

                return Page();
            }
            ModelState.AddModelError("All", "لطفا من ربات نیستم را تایید کنید");
            return Page();
        }
    }
}
