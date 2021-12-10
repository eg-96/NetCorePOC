using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreVueJsPOC.Entities.DataModel;

namespace NetCoreVueJsPOC.DAL.Constants
{
    public static class WeatherForecastConstants
    {
        private static readonly Random rng = new();

        public static readonly List<WeatherForecast> All = Enumerable.Range(1, 7).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            SummaryId = WeatherForecastSummaryConstants.All[rng.Next(WeatherForecastSummaryConstants.All.Count)].Id
        })
        .ToList();
    }
}
