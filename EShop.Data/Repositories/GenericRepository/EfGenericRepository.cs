using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EShop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repositories.GenericRepository
{
    public class EfGenericRepository<TModel> : IGenericRepository<TModel>, IDisposable where TModel : class
    {
        private EShopContext _context;

        public EfGenericRepository(EShopContext context)
        {
            _context = context;
        }

        #region Get All

        public async Task<IQueryable<TModel>> GetAllAsync(CancellationToken cancellationToken = default) => await Task.FromResult(_context.Set<TModel>());

        #endregion

        #region Finder

        public async Task<TModel> FindAsync(object uniqId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.FindAsync<TModel>(uniqId) ?? default(TModel);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task<bool> ExistAsync(Predicate<TModel> predicate, CancellationToken cancellationToken = default)
        {
            return (await (await GetAllAsync(cancellationToken)).ToListAsync(cancellationToken)).Exists(predicate);
        }

        #endregion

        #region Add

        public async Task AddAsync(TModel model, CancellationToken cancellationToken = default) => await _context.AddAsync(model, cancellationToken);
        public async Task AddAndSaveAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await AddAsync(model, cancellationToken);
            await SaveChangeAsync(cancellationToken);
        }

        #endregion

        #region Save

        public async Task SaveChangeAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

        #endregion

        #region Update

        public async Task UpdateAsync(TModel entity, CancellationToken cancellationToken = default)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
