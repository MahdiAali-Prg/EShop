using System;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Data.Repositories.GenericRepository
{
    public interface IFinderRepository<TModel>
    {
        public Task<TModel> FindAsync(object uniqId, CancellationToken cancellationToken = default);
        public Task<bool> ExistAsync(Predicate<TModel> predicate, CancellationToken cancellationToken = default);
    }
}
