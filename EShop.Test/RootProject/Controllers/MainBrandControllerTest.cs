using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Common.DTOs;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Test.Utilities.Repository;
using EShop.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.Controllers
{
    public class MainBrandControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // Arrange
            BrandController brandController = new BrandController(BrandRepository.Repository());

            // Act

            var result = await brandController.Index() as ViewResult;

            // Assert
            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(3, (brandController?.ViewData?.Model as ViewModelWithPageInfo<IEnumerable<Brand>>)?.PaginationInfo.TotalItems);
        }


        [Fact]
        public async Task DetailNotFondError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<Brand>>();
            repository.Setup(s => s.FindAsync(It.IsAny<object>(), default))
                .ReturnsAsync(default(Brand));
            BrandController brandController = new BrandController(repository.Object);

            // Act
            var result = await brandController.Detail(3) as NotFoundResult;
            
            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result?.StatusCode);
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
            Assert.Equal(1, (result?.ViewData?.Model as Brand)?.BrandId);
        }

        [Fact]
        public async Task ViewComponent()
        {
            // Arrange
            var viewComponentContext = new Mock<ViewComponentContext>();

            BrandController brandController = new BrandController(BrandRepository.Repository())
            {
                ComponentContext = new ViewComponentContext()
            };

            // Act
            var result = await brandController.InvokeAsync() as ViewViewComponentResult;

            // Assert
            Assert.Equal(3, (result.ViewData as ViewDataDictionary<IQueryable<Brand>>)?.Model.Count());
        }
    }
}
