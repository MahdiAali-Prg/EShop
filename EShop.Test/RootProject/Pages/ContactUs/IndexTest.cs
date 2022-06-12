using System;
using System.Collections.Generic;
using System.Text;
using EShop.Data.Models;
using EShop.Web.Pages.ContactUs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace EShop.Test.RootProject.Pages.ContactUs
{
    public class IndexTest
    {
        [Fact]
        public void Get()
        {
            // Arrange
            IndexModel indexModel = new IndexModel(null, default)
            {
                ContactUs = new Data.Models.ContactUs()
            };


            // Act
            var result = indexModel.OnGet() as PageResult;

            // Assert
            Assert.True(indexModel.ContactUs.HasResponse.Equals(false));
        }
    }
}
