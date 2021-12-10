using System.Linq;
using NetCoreVueJsPOC.DAL.Constants;

namespace NetCoreVueJsPOC.DAL
{
	public sealed partial class DbInitializer
	{
		public static void LoadWeatherForecasts(NetCoreVueJsPOCContext context)
		{
			if (!context.WeatherForecasts.Any())
            {
				foreach (var weatherForecast in WeatherForecastConstants.All)
				{
					var entity = context.WeatherForecasts.FirstOrDefault(x => x.Id == weatherForecast.Id);

					if (entity == null)
					{
						context.WeatherForecasts.Add(weatherForecast);
					}
					else
					{
						entity.TemperatureC = weatherForecast.TemperatureC;
						entity.SummaryId = weatherForecast.SummaryId;
						context.WeatherForecasts.Update(entity);
					}
				}
			}
		}
	}
}
