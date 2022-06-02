using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EShop.Data.Context;

namespace EShop.Data.Repositories.GenericRepository
{
    public class EfGenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private EShopContext _context;

        public EfGenericRepository(EShopContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<TModel>> GetAllAsync(CancellationToken cancellationToken = default) => await Task.FromResult(_context.Set<TModel>());

        public async Task<TModel> FindAsync(object uniqId, CancellationToken cancellationToken = default) =>
            await _context.FindAsync<TModel>(uniqId) ?? default(TModel);

        public async Task AddAsync(TModel model, CancellationToken cancellationToken = default) => await _context.AddAsync(model, cancellationToken);
        public async Task AddAndSaveAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await AddAsync(model, cancellationToken);
            await SaveChangeAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task UpdateAsync(TModel entity, CancellationToken cancellationToken = default)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
