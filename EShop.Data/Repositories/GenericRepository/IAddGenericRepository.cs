using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Data.Repositories.GenericRepository
{
    public interface IAddGenericRepository<TModel>
    {
        public Task AddAsync(TModel model, CancellationToken cancellationToken = default);
        public Task AddAndSaveAsync(TModel model, CancellationToken cancellationToken = default);
    }
}
