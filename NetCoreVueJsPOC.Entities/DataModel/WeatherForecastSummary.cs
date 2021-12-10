using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreVueJsPOC.Entities.DataModel
{
    public class WeatherForecastSummary
    {
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Utilities.Types.WeatherForecastSummaryId Id { get; set; }
        public string Label { get; set; }
    }
}
