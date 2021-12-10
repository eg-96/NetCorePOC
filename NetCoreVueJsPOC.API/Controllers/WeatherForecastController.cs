using Framework.API;
using Microsoft.AspNetCore.Mvc;
using NetCoreVueJsPOC.Entities.DataTransferObjectModel;
using NetCoreVueJsPOC.Services.Contracts;

namespace NetCoreVueJsPOC.API.Controllers
{
    [ApiController]
    [Route("api/WeatherForecast")]
    public class WeatherForecastController : ApiControllerBase<WeatherForecastDTO, WeatherForecastInsertDTO, WeatherForecastUpdateDTO, IWeatherForecastService>
    {
        public WeatherForecastController(IWeatherForecastService service) : base(service)
        {
        }
    }
}
