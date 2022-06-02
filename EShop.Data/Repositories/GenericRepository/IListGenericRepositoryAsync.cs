using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Data.Repositories.GenericRepository
{
    public interface IListGenericRepositoryAsync<TModel>
    {
        public Task<IQueryable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
