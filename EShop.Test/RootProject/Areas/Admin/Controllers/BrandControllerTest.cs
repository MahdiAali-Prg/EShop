using System.Collections.Generic;
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
    public class BrandControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result = (await brandController.Index(1) as ViewResult)?.ViewData?.Model as ViewModelWithPageInfo<IEnumerable<Brand>>;


            // Assert
            Assert.Equal(3, result?.ViewModel.Count());
        }

        [Fact]
        public void GetCreate()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            string viewName = (brandController.Create() as ViewResult)?.ViewName;

            // Assert
            Assert.Equal("Create", viewName);
        }

        [Fact]
        public async Task PostCreateNullImageError()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result = (await brandController.Create(new Brand() { Name = "B1", Description = "B-D1" }, null)) as ViewResult;

            // Assert
            Assert.Equal("Create", result?.ViewName);
            Assert.True(brandController.ModelState.ErrorCount == 1);
        }

        [Fact]
        public async Task PostCreateModelStateError()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            brandController.ModelState.AddModelError("All", "Invalid");

            // Act
            var result = (await brandController.Create(new Brand(), ImageFile.Image()) as ViewResult)?.ViewName;

            // Assert
            Assert.Equal("Create", result);
            Assert.False(brandController.ModelState.IsValid);
        }

        [Fact]
        public async Task PostCreate()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result =
                await brandController.Create(new Brand() { Name = "Brand Name", Description = "Brand Description" },
                    ImageFile.Image());

            // Assert
            Assert.Equal("Index", (result as RedirectToActionResult)?.ActionName);
        }


        [Fact]
        public async Task DetailError()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result = await brandController.Detail(default) as BadRequestResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
        }

        [Fact]
        public async Task Detail()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result = await brandController.Detail(1) as ViewResult;

            // Assert
            Assert.Equal("Detail", result?.ViewName);
            Assert.True((result?.ViewData?.Model as Brand)?.Name != null);
        }

        [Fact]
        public async Task GetEditBadRequestError()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result = await brandController.Edit(default) as BadRequestResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
        }

        [Fact]
        public async Task GetEditNotFondError()
        {
            // Arrange 
            var repository = new Mock<IGenericRepository<Brand>>();
            repository.Setup(s => s.FindAsync(It.IsAny<object>(), default))
                .ReturnsAsync(default(Brand));

            BrandController brandController = new BrandController(repository.Object);

            // Act

            var result = await brandController.Edit(1) as NotFoundResult;

            // Assert

            Assert.Equal(StatusCodes.Status404NotFound, result?.StatusCode);
        }

        [Fact]
        public async Task GetEdit()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act

            var result = await brandController.Edit(1) as ViewResult;

            // Assert
            Assert.Equal("Edit", result?.ViewName);
            Assert.Equal(1, (result?.ViewData?.Model as Brand)?.BrandId);
        }

        [Fact]
        public async Task PostEditModelStateError()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());
            brandController.ModelState.AddModelError("All", "Invalid Model Error !");
            // Act

            var result = await brandController.Edit(new Brand()
            {
                BrandId = 1,
                Name = "Test"
            }, null, default) as ViewResult;

            // Assert
            Assert.Equal("Edit", result?.ViewName);
        }

        [Fact]
        public async Task PostEdit()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act
            var result =
                await brandController.Edit(
                        new Brand() { BrandId = 1, Name = "B1", Description = "Bd1", Image = "test.png" }, null,
                        default)
                    as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result?.ActionName);
        }

    }
}
