using System.ComponentModel.DataAnnotations.Schema;
using NetCoreVueJsPOC.Utilities.Types;

namespace NetCoreVueJsPOC.Entities.DataModel
{
    public class WeatherForecastSummary
    {
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public WeatherForecastSummaryId Id { get; set; }
        public string Label { get; set; }
    }
}
