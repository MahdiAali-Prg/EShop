using System.Linq;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Data.Repositories.GenericRepository.GenericExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web.Areas.Admin.Components
{
    public class BlogCategoryProcessViewComponent : ViewComponent
    {
        private IGenericRepository<BlogCategory> _repository;

        public BlogCategoryProcessViewComponent(IGenericRepository<BlogCategory> repository)
        {
            _repository = repository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var model = await _repository.GetAllAsync();
            return View("Default", new BlogCategoryProcessViewModel()
            {
                ParentCategories = model.Count(c=>c.ParentId == null),
                ChildCategories = model.Count(c=>c.ParentId != null),
                CategoriesCount = model.Count()
            });
        }
    }
}
