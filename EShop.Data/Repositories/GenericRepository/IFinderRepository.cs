using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Data.Repositories.GenericRepository
{
    public interface IFinderRepository<TModel>
    {
        public Task<TModel> FindAsync(object uniqId, CancellationToken cancellationToken = default);
    }
}
