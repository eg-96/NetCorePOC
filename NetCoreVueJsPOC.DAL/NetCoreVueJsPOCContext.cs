using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using NetCoreVueJsPOC.Entities.DataModel;

namespace NetCoreVueJsPOC.DAL
{
    public class NetCoreVueJsPOCContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<WeatherForecastSummary> WeatherForecastSummaries { get; set; }

        public string DbPath { get; }

        public NetCoreVueJsPOCContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, " NetCoreVueJsPOC.db");
        }

        /// <summary>
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTableNamesSingular(modelBuilder);
        }

        /// <summary>
        /// Configure DB table names in singular and properties in plural
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ConfigureTableNamesSingular(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().ToTable(nameof(WeatherForecast));
            modelBuilder.Entity<WeatherForecastSummary>().ToTable(nameof(WeatherForecastSummary));
        }
    }
}
