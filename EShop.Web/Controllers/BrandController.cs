using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Data.Repositories.GenericRepository.GenericExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EShop.Web.Controllers
{
    [ViewComponent(Name = "BrandHybridViewComponent")]
    public class BrandController : Controller
    {
        private IGenericRepository<Brand> _repository;

        public BrandController(IGenericRepository<Brand> repository)
        {
            _repository = repository;
        }

        [Route("/Brand/{pageId?}")]
        public async Task<IActionResult> Index([FromRoute]int pageId = 1)
        {
            var model = await _repository.GetAllAsync();
            return View("Index", new ViewModelWithPageInfo<IEnumerable<Brand>>()
            {
                PaginationInfo = new PaginationInfo()
                {
                    TotalItems = model.Count(),
                    ItemPerPage = 3,
                    CurrentPage = pageId
                },
                ViewModel = model.Skip((pageId - 1) * 3).Take(3)
            });
        }

        [Route("/Brand/Detail/{id:long}")]
        public async Task<IActionResult> Detail([FromRoute] long id)
        {
            Brand brand = await _repository.FindAsync(id);
            if (brand is null)
            {
                return NotFound();
            }
            return View("Detail", brand);
        }

        [ViewComponentContext]
        public ViewComponentContext ComponentContext { get; set; }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var model = await _repository.GetAllAsync();
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<IQueryable<Brand>>(ComponentContext.ViewData, model.OrderByDescending(o=>o.BrandId).Take(3))
            };
        }
    }
}
