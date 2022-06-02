using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Data.Repositories.GenericRepository
{
    public interface IUpdateGenericRepository<TModel>
    {
        public Task UpdateAsync(TModel entity, CancellationToken cancellationToken = default);
    }
}
