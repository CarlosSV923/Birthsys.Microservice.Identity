using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Birthsys.Identity.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity, TId>(Context dbContext) where TEntity : Entity<TId> where TId : class
    {
        protected readonly Context _dbContext = dbContext;

        public virtual void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public virtual Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}