using System.Linq;
using EShop.Data.Models;
using EShop.Data.Repositories.GenericRepository;
using Moq;

namespace EShop.Test.Utilities.Repository
{
    public class BlogAuthorRepository
    {
        public static IGenericRepository<BlogAuthor> Repository()
        {
            var repository = new Mock<IGenericRepository<BlogAuthor>>();
            repository.Setup(s => s.GetAllAsync(default))
                .ReturnsAsync(new BlogAuthor[]
                {
                    new BlogAuthor()
                    {
                        BlogAuthorId = 1,
                        Name = "Author One"
                    },
                    new BlogAuthor()
                    {
                        BlogAuthorId = 2, 
                        Name = "Author Two"
                    }
                }.AsQueryable());

            repository.Setup(s => s.FindAsync((long)1, default))
                .ReturnsAsync(new BlogAuthor() { Name = "Name" });

            return repository.Object;
        }
    }
}
