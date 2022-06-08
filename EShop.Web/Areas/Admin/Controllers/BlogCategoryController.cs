using System.Threading.Tasks;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class BlogCategoryController : Controller
    {
        private IGenericRepository<BlogCategory> _repository;

        public BlogCategoryController(IGenericRepository<BlogCategory> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View("Index", await _repository.GetAllAsync());
        }

        #region Add New Blog Category

        [HttpGet]
        [HasIdParameter]
        public IActionResult Create([FromRoute] long? id)
        {
            return View("Create", new BlogCategory()
            {
                ParentId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm][Bind("Name,ParentId")] BlogCategory blogCategory)
        {
            if (blogCategory.ParentId != null && await _repository.FindAsync(blogCategory.ParentId) == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.AddAndSaveAsync(blogCategory);
                return RedirectToAction(nameof(Index));
            }

            return View("Create", blogCategory);
        }

        #endregion

        #region Edit Blog Category

        [HttpGet]
        [HasIdParameter]
        public async Task<IActionResult> Edit(long id)
        {
            if (id == default || id < 0)
            {
                return BadRequest();
            }

            BlogCategory blogCategory = await _repository.FindAsync(id);
            if (blogCategory != null)
            {
                return View("Edit", blogCategory);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm][Bind("BlogCategoryId,Name,ParentId")]BlogCategory blogCategory)
        {
            if (await _repository.FindAsync(blogCategory.BlogCategoryId) is null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(blogCategory);
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", blogCategory);
        }

        #endregion


        #region Remote Validation

        public async Task<bool> NameValidation(string name)
        {
            return !await _repository.ExistAsync(p => p.Name == name);
        }

        #endregion

    }
}
