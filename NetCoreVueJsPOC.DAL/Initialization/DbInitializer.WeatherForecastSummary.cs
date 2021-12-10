using System.Linq;
using NetCoreVueJsPOC.DAL.Constants;

namespace NetCoreVueJsPOC.DAL
{
	public sealed partial class DbInitializer
	{
		public static void LoadWeatherForecastSummaries(NetCoreVueJsPOCContext context)
		{
			foreach (var weatherForecastSummary in WeatherForecastSummaryConstants.All)
            {
				var entity = context.WeatherForecastSummaries.FirstOrDefault(x => x.Id == weatherForecastSummary.Id);

				if (entity == null)
                {
					context.WeatherForecastSummaries.Add(weatherForecastSummary);
				}
				else
                {
					entity.Label = weatherForecastSummary.Label;
					context.WeatherForecastSummaries.Update(entity);
				}
            }
		}
	}
}
