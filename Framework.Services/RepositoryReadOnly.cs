using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Services.Contracts;
using Framework.Utilities.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Framework.Services
{
    public class RepositoryReadOnly<TContext> : IRepositoryReadOnly
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public RepositoryReadOnly(TContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            includeProperties ??= string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        public virtual IQueryable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return GetQueryable(filter, includeProperties, orderBy, skip, take);
        }

        public virtual IQueryable<TEntity> GetAll<TEntity>(
            string includeProperties = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return GetQueryable(null, includeProperties, orderBy, skip, take);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).AnyAsync();
        }

        public virtual Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return GetQueryable(filter, includeProperties, orderBy, skip, take).FirstOrDefaultAsync();
        }

        public virtual TEntity GetFirstOurDefault<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return GetQueryable(filter, includeProperties, orderBy, skip, take).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> GetLazyLoading<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            var includePropertyList = Activator.CreateInstance<TEntity>().GetListProperties();
            var includeProperties = string.Join(",", includePropertyList);

            return GetQueryable(filter, includeProperties, orderBy, skip, take);
        }
    }
}
