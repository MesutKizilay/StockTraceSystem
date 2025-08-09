using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
    {
        protected readonly TContext Context;

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (include != null)
                queryable = include(queryable);

            return await queryable.FirstOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<List<TEntity>> GetList(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (include != null)
                queryable = include(queryable);

            return filter == null
                ? await queryable.ToListAsync(cancellationToken: cancellationToken)
                : await queryable.Where(filter).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            //Context.ChangeTracker.Clear();
            Context.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> Query() => Context.Set<TEntity>();

        public async Task<Paginate<TEntity>> GetListWithPaginate(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Expression<Func<TEntity, bool>>? filter = null, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (include != null)
                queryable = include(queryable);
            if (filter != null)
                queryable = queryable.Where(filter);

            return await queryable.ToPaginateAsync(index, size, cancellationToken);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            return await queryable.AnyAsync(cancellationToken);
        }
    }
}
