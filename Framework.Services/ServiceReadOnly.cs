using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Framework.Services
{
    public class ServiceReadOnly<TEntity, TEntityDTO> : IServiceReadOnly<TEntityDTO>
        where TEntity : class
    {
        protected readonly IRepository _repo;
        protected readonly IMapper _mapper;

        public ServiceReadOnly(DbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _repo = new Repository<DbContext>(dbContext);
        }

        private Expression<Func<TEntity, bool>> GetLambdaForGetForId(int id)
        {
            var pe = Expression.Parameter(typeof(TEntity), "x");
            var idProperty = Expression.Property(pe, "Id");
            var idParameterValue = Expression.Constant(id, typeof(int));
            var body = Expression.Equal(idProperty, idParameterValue);
            return Expression.Lambda<Func<TEntity, bool>>(body, pe);
        }

        protected async Task<TEntity> GetForIdDbEntity(int id)
            => await _repo.Get<TEntity>()
                .Where(GetLambdaForGetForId(id))
                .FirstOrDefaultAsync();

        protected async Task<TEntity> GetForIdDbEntityLazyLoading(int id)
            => await _repo.GetLazyLoading<TEntity>()
                .Where(GetLambdaForGetForId(id))
                .FirstOrDefaultAsync();

        public virtual async Task<bool> ExistForId(int id)
            => await _repo.GetExistsAsync(GetLambdaForGetForId(id));

        public virtual async Task<IEnumerable<TEntityDTO>> Get()
            => await _repo.GetAll<TEntity>()
                .ProjectTo<TEntityDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public virtual async Task<int> GetCount()
            => await _repo.GetCountAsync<TEntity>();

        public virtual async Task<int> GetCount(Expression<Func<TEntity, bool>> condition)
            => await _repo.GetCountAsync(condition);

        public virtual async Task<TEntityDTO> GetForId(int id)
            => await _repo.Get<TEntity>()
                .Where(GetLambdaForGetForId(id))
                .ProjectTo<TEntityDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
    }
}
