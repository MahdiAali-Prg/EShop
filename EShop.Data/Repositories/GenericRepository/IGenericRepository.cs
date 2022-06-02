using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Data.Repositories.GenericRepository
{
    public interface IGenericRepository<TModel> : IListGenericRepositoryAsync<TModel>
        ,IAddGenericRepository<TModel>, IFinderRepository<TModel>, IUpdateGenericRepository<TModel>
        where TModel : class
    {
        public Task SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
