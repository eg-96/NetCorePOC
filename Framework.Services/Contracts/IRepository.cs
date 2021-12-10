using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Services.Contracts
{
    public interface IRepository : IRepositoryReadOnly
    {
        EntityEntry<TEntity> Create<TEntity>(TEntity entity)
            where TEntity : class;

        void Update<TEntity>(TEntity entity)
            where TEntity : class;

        void Delete<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        void AttachUnchanged<TEntity>(TEntity toAttach)
            where TEntity : class;

        void Save();

        Task SaveAsync();
    }
}
