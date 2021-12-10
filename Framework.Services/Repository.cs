using System;
using System.Threading.Tasks;
using Framework.Services.Contracts;
using Framework.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Services
{
    public class Repository<TContext> : RepositoryReadOnly<TContext>, IRepository
        where TContext : DbContext
    {
        public Repository(TContext context) : base(context)
        {
        }

        public void AttachUnchanged<TEntity>(TEntity toAttach) where TEntity : class
        {
            _context.Entry(toAttach).State = EntityState.Unchanged;
            _context.Set<TEntity>().Attach(toAttach);
        }

        public EntityEntry<TEntity> Create<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(object id) where TEntity : class
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();

            if (_context.Entry(entity).State == EntityState.Detached)
                dbSet.Remove(entity);

            dbSet.Remove(entity);

        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public Task SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        protected virtual void ThrowEnhancedValidationException(Exception e)
        {
            var exceptionMessage = string.Concat(e.Message, "The inner exception is: ", e.InnerException?.Message);
            var exceptionToCatchAndThrow = new Exception(exceptionMessage);

            exceptionToCatchAndThrow.Manage();

            throw exceptionToCatchAndThrow;
        }
    }
}
