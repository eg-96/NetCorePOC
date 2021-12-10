using AutoMapper;
using Framework.Services;
using NetCoreVueJsPOC.DAL;
using NetCoreVueJsPOC.Entities.DataModel;
using NetCoreVueJsPOC.Entities.DataTransferObjectModel;
using NetCoreVueJsPOC.Services.Contracts;

namespace NetCoreVueJsPOC.Services
{
    public class WeatherForecastService : Service<WeatherForecast, WeatherForecastDTO, WeatherForecastInsertDTO, WeatherForecastUpdateDTO>, IWeatherForecastService
    {
        public WeatherForecastService(NetCoreVueJsPOCContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
