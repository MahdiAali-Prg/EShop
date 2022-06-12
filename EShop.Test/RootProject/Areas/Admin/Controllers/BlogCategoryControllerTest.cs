using System.Linq;
using System.Threading.Tasks;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Test.Utilities.Repository;
using EShop.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.Areas.Admin.Controllers
{
    public class BlogCategoryControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = await blogCategoryController.Index() as ViewResult;

            // Assert
            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(2, (result?.ViewData?.Model as IQueryable<BlogCategory>)?.Count());
        }

        #region Create

        [Fact]
        public void GetCreate()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = blogCategoryController.Create(default(long)) as ViewResult;

            // Assert
            Assert.Equal("Create", result?.ViewName);
        }

        [Fact]
        public void GetCreateWithData()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = blogCategoryController.Create(1) as ViewResult;

            //Assert
            Assert.Equal("Create", result?.ViewName);
            Assert.Equal(1, (result?.ViewData?.Model as BlogCategory)?.ParentId);
        }

        [Fact]
        public async Task PostCreateBadRequestError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<BlogCategory>>();
            repository.Setup(s => s.FindAsync((long)1, default))
                .ReturnsAsync(new BlogCategory() { BlogCategoryId = 1, Name = "CAT 1" });

            BlogCategoryController blogCategoryController = new BlogCategoryController(repository.Object);

            // Act
            var result = await blogCategoryController.Create(new BlogCategory() { Name = "Test", ParentId = 2 }) as BadRequestResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
        }

        [Fact]
        public async Task ModelStateError()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            blogCategoryController.ModelState.AddModelError("All", "Test");

            // Act
            var result = await blogCategoryController.Create(new BlogCategory()) as ViewResult;

            // Assert
            Assert.Equal("Create", result?.ViewName);
        }

        [Fact]
        public async Task PostCreate()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = await blogCategoryController.Create(new BlogCategory() { Name = "Test", ParentId = 1 }) as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result?.ActionName);
        }

        #endregion

        #region Edit

        [Fact]
        public async Task GetEditNotFondError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<BlogCategory>>();
            repository.Setup(s => s.FindAsync(1, default))
                .ReturnsAsync(new BlogCategory()
                {
                    BlogCategoryId = 1
                });
            BlogCategoryController blogCategoryController = new BlogCategoryController(repository.Object);

            // Act
            var result = await blogCategoryController.Edit(2) as NotFoundResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result?.StatusCode);
        }

        [Fact]
        public async Task GetEdit()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = await blogCategoryController.Edit(1) as ViewResult;

            // Assert
            Assert.Equal("Edit", result?.ViewName);
        }

        [Fact]
        public async Task PostEditBadRequestError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<BlogCategory>>();
            repository.Setup(s => s.FindAsync(1, default))
                .ReturnsAsync(new BlogCategory() { BlogCategoryId = 1 });

            BlogCategoryController blogCategoryController = new BlogCategoryController(repository.Object);
            // Act

            var result = await blogCategoryController.Edit(new BlogCategory() { BlogCategoryId = 2 }) as BadRequestResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
        }

        [Fact]
        public async Task PostEditModelStateValidationError()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());
            
            // Act
            blogCategoryController.ModelState.AddModelError("All", "Error");

            var result = await blogCategoryController.Edit(new BlogCategory()) as ViewResult;

            // Assert
            Assert.Equal("Edit", result?.ViewName);
        }

        [Fact]
        public async Task PostEdit()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = await blogCategoryController.Edit(new BlogCategory() { BlogCategoryId = 1, Name = "Test"}) as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result?.ActionName);
        }

        #endregion-

        #region Remote Validation

        [Fact]
        public async Task RemoteValidation()
        {
            // Arrange
            BlogCategoryController blogCategoryController = new BlogCategoryController(BlogCategoryRepository.Repository());

            // Act
            var result = await blogCategoryController.NameValidation("test");

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}
