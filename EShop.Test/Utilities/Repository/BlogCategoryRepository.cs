using System;
using System.Linq;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using Moq;

namespace EShop.Test.Utilities.Repository
{
    internal class BlogCategoryRepository
    {
        public static IGenericRepository<BlogCategory> Repository()
        {
            var repository = new Mock<IGenericRepository<BlogCategory>>();
            repository.Setup(s => s.GetAllAsync(default))
                .ReturnsAsync(new BlogCategory[]
                {
                    new BlogCategory(){BlogCategoryId = 1, Name = "B1"},
                    new BlogCategory(){BlogCategoryId = 2, Name = "BP1", ParentId = 1}
                }.AsQueryable());

            repository.Setup(s => s.FindAsync(It.IsAny<object>(), default))
                .ReturnsAsync(new BlogCategory()
                {
                    BlogCategoryId = 1, 
                    Name = "Test"
                });

            repository.Setup(s => s.ExistAsync(It.IsAny<Predicate<BlogCategory>>(), default))
                .ReturnsAsync(true);
            return repository.Object;
        }
    }
}
