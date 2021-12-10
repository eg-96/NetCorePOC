using Framework.Services.Contracts;
using NetCoreVueJsPOC.Entities.DataTransferObjectModel;

namespace NetCoreVueJsPOC.Services.Contracts
{
    public interface IWeatherForecastService : IService<WeatherForecastDTO, WeatherForecastInsertDTO, WeatherForecastUpdateDTO>
    {
        
    }
}
