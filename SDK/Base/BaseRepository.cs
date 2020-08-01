using Microsoft.EntityFrameworkCore;
using SDK.Base.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SDK.Base
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            _dbSet.Remove(await SelectByIdAsync(id, cancellationToken));
        }

        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken) =>
            await _dbSet.AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (await ExistsAsync(entity.Id, cancellationToken)) return;

            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task<TEntity> SelectByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        public virtual IQueryable<TEntity> SelectAll() => _dbSet.AsNoTracking();

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (!await ExistsAsync(entity.Id, cancellationToken)) return;

            _dbSet.Update(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}