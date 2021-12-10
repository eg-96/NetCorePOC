using System.Threading.Tasks;

namespace Framework.Services.Contracts
{
    public interface IService<TEntityDTO, in TEntityInsertDTO, in TEntityUpdateDTO> : IServiceReadOnly<TEntityDTO>
    {
        Task<TEntityDTO> Insert(TEntityInsertDTO toInsert);
        Task<TEntityDTO> Update(int id, TEntityUpdateDTO toUpdate);
        Task Delete(int id);
    }
}
