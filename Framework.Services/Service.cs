using System;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Services.Contracts;
using Framework.Services.Internal;
using Framework.Utilities.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Framework.Services
{
    public class Service<TEntity, TEntityDTO, TEntityInsertDTO, TEntityUpdateDTO> : ServiceReadOnly<TEntity, TEntityDTO>, IService<TEntityDTO, TEntityInsertDTO, TEntityUpdateDTO>
        where TEntity : class
    {
        protected Service(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetForIdDbEntity(id);

            if (entity == null)
                throw new Exception(Constants.DB_DELETE_INCONGRUENCE);

            _repo.Delete(entity);

            await _repo.SaveAsync();
        }

        public virtual async Task<TEntityDTO> Insert(TEntityInsertDTO toInsert)
        {
            var entity = _mapper.Map<TEntityInsertDTO, TEntity>(toInsert);

            entity = _repo.Create(entity)?.Entity;

            await _repo.SaveAsync();

            TEntityDTO toReturn;
            if (entity.HasProperty("Id"))
                toReturn = await GetForId(entity.GetValueOfProperty<TEntity, int>("id"));
            else
                toReturn = _mapper.Map<TEntity, TEntityDTO>(entity);

            return toReturn;
        }

        public virtual async Task<TEntityDTO> Update(int id, TEntityUpdateDTO toUpdate)
        {
            if (toUpdate.HasProperty("Id"))
                if (toUpdate.GetValueOfProperty<TEntityUpdateDTO, int>("id") != id)
                    throw new Exception(Constants.DB_UPDATE_INCONGRUENCE);

            var entity = await GetForIdDbEntityLazyLoading(id);

            entity = _mapper.Map(toUpdate, entity);

            await _repo.SaveAsync();

            TEntityDTO toReturn;
            if (entity.HasProperty("Id"))
                toReturn = await GetForId(entity.GetValueOfProperty<TEntity, int>("id"));
            else
                toReturn = _mapper.Map<TEntity, TEntityDTO>(entity);

            return toReturn;
        }
    }
}
