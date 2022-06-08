using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Common.Services.Image;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Web.Filters;
using EShop.Web.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class BlogAuthorController : Controller
    {
        private IGenericRepository<BlogAuthor> _repository;

        public BlogAuthorController(IGenericRepository<BlogAuthor> repository)
        {
            _repository = repository;
        }

        [Route("/Admin/BlogAuthor/{pageId:positive?}")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int pageId = 1)
        {
            var model = await _repository.GetAllAsync(cancellationToken);
            return View("Index", new ViewModelWithPageInfo<IQueryable<BlogAuthor>>()
            {
                ViewModel = model.Skip((pageId - 1) * 4).Take(4),
                PaginationInfo = new PaginationInfo()
                {
                    CurrentPage = pageId,
                    ItemPerPage = 4,
                    TotalItems = model.Count()
                }
            });
        }

        #region Create New Author

        [HttpGet]
        public IActionResult Create() => View("Create");

        [HttpPost]
        public async Task<IActionResult> Create([FromForm][Bind("Name,ShortDescription")] BlogAuthor blogAuthor, [FromForm] IFormFile authorImage, CancellationToken token)
        {
            if (!ImageCheck(authorImage))
            {
                return View("Create");
            }

            blogAuthor.RegisterDate = DateTime.Now;
            blogAuthor.Image = ImageNameGenerator.Generate();
            if (ModelState.IsValid)
            {
                await authorImage.SaveAsync("AuthorImage", blogAuthor, token);
                await _repository.AddAndSaveAsync(blogAuthor, token);
                return RedirectToAction(nameof(Index));
            }
            return View("Create");
        }

        #endregion

        #region Author Detail

        [HttpGet]
        [HasIdParameter]
        public async Task<IActionResult> Detail([FromRoute]long id, CancellationToken cancellationToken)
        {
            var author = await _repository.FindAsync(id, cancellationToken);
            if (author is null)
            {
                return NotFound();
            }
            return View("Detail", author);
        }

        #endregion

        #region Edit Author

        [HasIdParameter]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]long id, CancellationToken cancellationToken)
        {
            var blogAuthor = await _repository.FindAsync(id, cancellationToken);
            if (blogAuthor != null)
            {
                return View("Edit", blogAuthor);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm][Bind("BlogAuthorId,Name,ShortDescription,Image,RegisterDate")]BlogAuthor blogAuthor,[FromForm] IFormFile authorImage, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (ImageCheck(authorImage))
                {
                    await authorImage.SaveAsync("AuthorImage", blogAuthor, cancellationToken);
                }

                await _repository.UpdateAsync(blogAuthor, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", blogAuthor);
        }

        #endregion


        #region ImageValidation


        [NonAction]
        private bool ImageCheck(IFormFile image)
        {
            ModelState.Remove("Image");
            if (ImageValidator.IsImageNull(image))
            {
                ModelState.AddModelError("All", "تصویر برند نمیتواند خالی باشد");
                return false;
            }
            else if (!ImageValidator.IsImageExtensionPng(image))
            {
                ModelState.AddModelError("All", "پسوند فایل معتبر نیست . فقط فرمت (*.png) مورد قبول است");
                return false;
            }
            else if (!ImageValidator.IsImageSizeValid(image))
            {
                ModelState.AddModelError("All", "حجم تصویر بیشتر از حد مجاز است. حجم مورد قبول (1MB) میباشد");
                return false;
            }

            return true;
        }

        #endregion
    }
}
