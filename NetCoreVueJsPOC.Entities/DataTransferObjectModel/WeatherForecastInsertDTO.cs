using System;
namespace NetCoreVueJsPOC.Entities.DataTransferObjectModel
{
    public class WeatherForecastInsertDTO
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public WeatherForecastSummaryDTO Summary { get; set; }
    }
}
