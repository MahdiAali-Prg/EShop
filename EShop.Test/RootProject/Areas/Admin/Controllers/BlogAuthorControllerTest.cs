using System.Linq;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Test.Utilities.Images;
using EShop.Test.Utilities.Repository;
using EShop.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.Areas.Admin.Controllers
{
    public class BlogAuthorControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result = await blogAuthorController.Index(default) as ViewResult;

            // Assert
            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(2, (result?.ViewData?.Model as ViewModelWithPageInfo<IQueryable<BlogAuthor>>)?.ViewModel.Count());

        }

        #region Create

        [Fact]
        public void GetCreate()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result = blogAuthorController.Create() as ViewResult;

            // Assert
            Assert.Equal("Create", result?.ViewName);
        }

        [Fact]
        public async Task PostCreateImageError()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result = await blogAuthorController.Create(new BlogAuthor(), null, default) as ViewResult;

            // Assert
            Assert.Equal("Create", result?.ViewName);
        }

        [Fact]
        public async Task PostCreateModelStateError()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            blogAuthorController.ModelState.AddModelError("All", "Invalid Model Error");
            var result = await blogAuthorController.Create(new BlogAuthor(), ImageFile.Image(), default) as ViewResult;

            // Assert
            Assert.Equal("Create", result?.ViewName);
        }

        [Fact]
        public async Task PostCreate()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result =
                await blogAuthorController.Create(new BlogAuthor() { Name = "name", ShortDescription = "short" },
                    ImageFile.Image(), default) as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result?.ActionName);
        }

        #endregion

        #region Detail

        [Fact]
        public async Task DetailNotFoundError()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result = await blogAuthorController.Detail(3, default) as NotFoundResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result?.StatusCode);
        }

        [Fact]
        public async Task Detail()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result = await blogAuthorController.Detail(1, default) as ViewResult;

            // Assert
            Assert.Equal("Detail", result?.ViewName);
        }

        #endregion

        #region Edit

        [Fact]
        public async Task GetEditNotFound()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<BlogAuthor>>();
            repository.Setup(s => s.FindAsync(1, default))
                .ReturnsAsync(new BlogAuthor() { BlogAuthorId = 1 });

            BlogAuthorController blogAuthorController = new BlogAuthorController(repository.Object);
            // Act
            var result = await blogAuthorController.Edit(2, default) as NotFoundResult;

            // Assert
            Assert.True(result?.StatusCode is StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetEdit()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result = await blogAuthorController.Edit(1, default) as ViewResult;

            // Assert
            Assert.True(result?.ViewName is "Edit");
        }

        [Fact]
        public async Task PostEditModelStateError()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            blogAuthorController.ModelState.AddModelError("All", "Test Error");
            var result = await blogAuthorController.Edit(new BlogAuthor(), ImageFile.Image(), default) as ViewResult;

            // Assert
            Assert.True(result?.ViewName is "Edit");
        }

        [Fact]
        public async Task PostEdit()
        {
            // Arrange
            BlogAuthorController blogAuthorController = new BlogAuthorController(BlogAuthorRepository.Repository());

            // Act
            var result =
                await blogAuthorController.Edit(new BlogAuthor() { BlogAuthorId = 1, Image = "TestImage"}, ImageFile.Image(), default) as
                    RedirectToActionResult;

            // Assert
            Assert.True(result?.ActionName is "Index");

        }

        #endregion

    }
}
