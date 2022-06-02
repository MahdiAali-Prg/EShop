using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using Moq;

namespace EShop.Test.Utilities.Repository
{
    internal class BrandRepository
    {
        public static IGenericRepository<Brand> Repository(CancellationToken cancellationToken = default)
        {
            var repository = new Mock<IGenericRepository<Brand>>();
            repository.Setup(s => s.GetAllAsync(cancellationToken))
                .ReturnsAsync(new List<Brand>()
                {
                    new Brand() { BrandId = 1, Name = "Brand 1", Description = "Brand Description 1", Image = "image path"},
                    new Brand() { BrandId = 2, Name = "Brand 2", Description = "Brand Description 2", Image = "image path"},
                    new Brand() { BrandId = 3, Name = "Brand 3", Description = "Brand Description 3", Image = "image path"}
                }.AsQueryable());
            repository.Setup(s => s.FindAsync(It.IsAny<object>(), cancellationToken))
                .ReturnsAsync(new Brand()
                {
                    BrandId = 1,
                    Name = "Brand Name"
                });

            return repository.Object;
        }
    }
}
