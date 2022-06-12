using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Common.DTOs.ContactUs;
using EShop.Common.Services.EmailTools;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        private IGenericRepository<ContactUs> _repository;

        public ContactUsController(IGenericRepository<ContactUs> repository)
        {
            _repository = repository;
        }

        [Route("/Admin/ContactUs/{pageId:long:positive?}")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, long? pageId = 1)
        {
            var model = await _repository.GetAllAsync(cancellationToken);
            return View("Index", new ViewModelWithPageInfo<IEnumerable<ContactUs>>()
            {
                ViewModel = model.Skip((int)(pageId - 1) * 10).Take(10).OrderByDescending(o=>o.ContactUsId),
                PaginationInfo = new PaginationInfo()
                {
                    CurrentPage = (int)pageId,
                    ItemPerPage = 10,
                    TotalItems = model.Count()
                }
            });
        }

        [HasIdParameter]
        public async Task<IActionResult> Detail([FromRoute]long id, CancellationToken cancellationToken)
        {
            var contactUs = await _repository.FindAsync(id, cancellationToken);
            if (contactUs is null)
            {
                return NotFound();
            }

            return View("Detail", contactUs);
        }

        [HttpGet]
        [HasIdParameter]
        public async Task<IActionResult> MessageResponse([FromRoute]long id, CancellationToken cancellationToken)
        {
            var contactUs = await _repository.FindAsync(id, cancellationToken);
            if (contactUs is null || contactUs.HasResponse)
            {
                return NotFound();
            }

            ViewData["Message"] = contactUs.Message;
            return View("MessageResponse", new ContactUsResponseViewModel()
            {
                MessageId = contactUs.ContactUsId,
                Email = contactUs.Email
            });
        }

        [HttpPost]
        [HasIdParameter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MessageResponse([FromRoute] long id,[FromForm]ContactUsResponseViewModel model, CancellationToken cancellationToken)
        {
            var contactUs = await _repository.FindAsync(id, cancellationToken);
            if (model.MessageId.Equals(id) && model.Email.Equals(contactUs.Email))
            {
                if (ModelState.IsValid)
                {
                    if (await EmailSender.SendAsync(model.Email, model.Response, "پاسخ به تماس شما در سایت"))
                    {
                        contactUs.HasResponse = true;
                        await _repository.UpdateAsync(contactUs, cancellationToken);
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("All", "ایمیل ارسال نشد دوباره تلاش کنید");
                    return View("MessageResponse", model);
                }
                ModelState.AddModelError("All", "پاسخ نمیتواند خالی باشد");
                return View("MessageResponse", model);
            }

            return BadRequest();
        }
    }
}
