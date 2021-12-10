using System;
using NetCoreVueJsPOC.Utilities.Types;

namespace NetCoreVueJsPOC.Entities.DataModel
{
    public class WeatherForecast : BaseEntity
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public WeatherForecastSummaryId SummaryId { get; set; }
        public WeatherForecastSummary Summary { get; set; }
    }
}
