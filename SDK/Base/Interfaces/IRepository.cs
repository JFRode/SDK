using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SDK.Base.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

        IQueryable<TEntity> SelectAll();

        Task<TEntity> SelectByIdAsync(Guid id, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancelletionToken);

        void Commit();
    }
}