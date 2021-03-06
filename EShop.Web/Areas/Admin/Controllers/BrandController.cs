using System.Collections.Generic;
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
    [AutoValidateAntiforgeryToken]
    [Area("Admin")]
    public class BrandController : Controller
    {
        private IGenericRepository<Brand> _repository;

        public BrandController(IGenericRepository<Brand> repository)
        {
            _repository = repository;
        }

        [Route("/Admin/Brand/{pageId:positive?}")]
        public async Task<IActionResult> Index([FromRoute] int pageId = 1, CancellationToken cancellationToken = default)
        {
            var model = await _repository.GetAllAsync(cancellationToken);

            return View("Index", new ViewModelWithPageInfo<IEnumerable<Brand>>()
            {
                PaginationInfo = new PaginationInfo()
                {
                    ItemPerPage = 3,
                    TotalItems = model.Count(),
                    CurrentPage = pageId
                },
                ViewModel = model.Skip((pageId - 1) * 3).Take(3)
            });
        }

        #region Create New Brand

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm][Bind("Name,Description,Image")] Brand brand,
            [FromForm] IFormFile brandImage, CancellationToken cancellationToken = default)
        {
            if (!ImageCheck(brandImage))
            {
                return View("Create", brand);
            }

            if (ModelState.IsValid)
            {
                brand.Image = ImageNameGenerator.Generate();
                await brandImage.SaveAsync("BrandImage", brand, cancellationToken);
                await _repository.AddAndSaveAsync(brand, cancellationToken);
                return RedirectToAction(nameof(Index));
            }

            return View("Create", brand);
        }

        #endregion

        #region Brand Detail


        [HasIdParameter]
        public async Task<IActionResult> Detail([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            Brand brand = await _repository.FindAsync(id, cancellationToken);
            if (brand != null)
            {
                return View("Detail", brand);
            }

            return NotFound();
        }
        #endregion

        #region Edit Brand

        [HttpGet]
        [HasIdParameter]
        public async Task<IActionResult> Edit([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            Brand brand = await _repository.FindAsync(id, cancellationToken);
            if (brand != null)
            {
                return View("Edit", brand);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm][Bind("BrandId,Name,Description,Image")] Brand brand,
            [FromForm] IFormFile brandImage, CancellationToken cancellationToken)
        {
            if (brandImage != null && ImageCheck(brandImage))
            {
                await brandImage.SaveAsync("BrandImage", brand, cancellationToken);
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(brand, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("All", "لطفا همه فیلد ها را پر کنید");
            return View("Edit", brand);
        }

        #endregion


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
    }
}
