using System;
using System.Collections.Generic;
using NetCoreVueJsPOC.Entities.DataModel;
using NetCoreVueJsPOC.Utilities.Types;

namespace NetCoreVueJsPOC.DAL.Constants
{
    public static class WeatherForecastSummaryConstants
    {
        public static readonly List<WeatherForecastSummary> All = new List<WeatherForecastSummary>
        {
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Freezing, Label = WeatherForecastSummaryId.Freezing.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Bracing, Label = WeatherForecastSummaryId.Bracing.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Chilly, Label = WeatherForecastSummaryId.Chilly.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Cool, Label = WeatherForecastSummaryId.Cool.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Mild, Label = WeatherForecastSummaryId.Mild.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Warm, Label = WeatherForecastSummaryId.Warm.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Balmy, Label = WeatherForecastSummaryId.Balmy.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Hot, Label = WeatherForecastSummaryId.Hot.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Sweltering, Label = WeatherForecastSummaryId.Sweltering.ToString() },
            new WeatherForecastSummary { Id = WeatherForecastSummaryId.Scorching, Label = WeatherForecastSummaryId.Scorching.ToString() }
        };
    }
}
