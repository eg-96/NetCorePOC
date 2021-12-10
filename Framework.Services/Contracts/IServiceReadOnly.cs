using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Services.Internal;

namespace Framework.Services.Contracts
{
    public interface IServiceReadOnly<TEntityDTO> : IServiceBaseIndicator
    {
        Task<IEnumerable<TEntityDTO>> Get();
        Task<TEntityDTO> GetForId(int id);
        Task<bool> ExistForId(int id);
        Task<int> GetCount();
    }
}
