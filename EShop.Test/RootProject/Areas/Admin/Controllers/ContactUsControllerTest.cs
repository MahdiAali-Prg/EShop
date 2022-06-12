using System.Linq;
using System.Threading.Tasks;
using EShop.Common.DTOs.ContactUs;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using EShop.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EShop.Test.RootProject.Areas.Admin.Controllers
{
    public class ContactUsControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.GetAllAsync(default))
                .ReturnsAsync(new ContactUs[]
                {
                    new ContactUs()
                    {
                        Email = "Test@test.com",
                        Message = "test"
                    }
                }.AsQueryable());
            ContactUsController contactUsController = new ContactUsController(repository.Object);

            // Act
            var result = await contactUsController.Index(default) as ViewResult;

            // Assert
            Assert.True(result?.ViewName.Equals("Index"));
        }

        [Fact]
        public async Task DetailNotFond()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync((long)2, default))
                .ReturnsAsync(new ContactUs());

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            var result = await contactUsController.Detail(1, default) as NotFoundResult;

            // Assert
            Assert.True(result?.StatusCode.Equals(StatusCodes.Status404NotFound));
        }

        [Fact]
        public async Task Detail()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync(It.IsAny<long>(), default))
                .ReturnsAsync(new ContactUs() { Email = "example@gmail.com"});

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            var result = await contactUsController.Detail(1, default) as ViewResult;

            // Assert
            Assert.True(result?.ViewName.Equals("Detail"));
        }

        [Fact]
        public async Task GetResponseNotFondError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync((long)2, default))
                .ReturnsAsync(new ContactUs());

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            var result = await contactUsController.MessageResponse(1, default) as NotFoundResult;

            // Assert
            Assert.True(result?.StatusCode.Equals(StatusCodes.Status404NotFound));
        }

        [Fact]
        public async Task GetResponse()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync(It.IsAny<long>(), default))
                .ReturnsAsync(new ContactUs() { Email = "example@gmail.com" });

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            var result = await contactUsController.MessageResponse(1, default) as ViewResult;

            // Assert
            Assert.True(result?.ViewName.Equals("MessageResponse"));
        }

        [Fact]
        public async Task PostMessageResponseBadRequestError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync(It.IsAny<long>(), default))
                .ReturnsAsync(new ContactUs() { Email = "example@gmail.com" });

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            var result = await contactUsController.MessageResponse(1, new ContactUsResponseViewModel()
            {
                Email = "example@example.com",
                MessageId = 2,
                Response = "test"
            }, default) as BadRequestResult;

            // Assert
            Assert.True(result?.StatusCode.Equals(StatusCodes.Status400BadRequest));
        }

        [Fact]
        public async Task PostMessageResponseModelStateError()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync(It.IsAny<long>(), default))
                .ReturnsAsync(new ContactUs() { Email = "example@gmail.com" });

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            contactUsController.ModelState.AddModelError("All", "Test");
            var result = await contactUsController.MessageResponse(1, new ContactUsResponseViewModel()
            {
                Email = "example@gmail.com",
                MessageId = 1,
                Response = "test"
            }, default) as ViewResult;

            // Assert
            Assert.True(result?.ViewName.Equals("MessageResponse"));
        }

        [Fact]
        public async Task PostMessageResponse()
        {
            // Arrange
            var repository = new Mock<IGenericRepository<ContactUs>>();
            repository.Setup(s => s.FindAsync(It.IsAny<long>(), default))
                .ReturnsAsync(new ContactUs() { Email = "example@gmail.com" });

            ContactUsController contactUsController = new ContactUsController(repository.Object);
            // Act
            var result = await contactUsController.MessageResponse(1, new ContactUsResponseViewModel()
            {
                Email = "example@gmail.com",
                MessageId = 1,
                Response = "test"
            }, default) as RedirectToActionResult;

            // Assert
            Assert.True(result?.ActionName.Equals("Index"));
        }

    }
}
