using Microsoft.EntityFrameworkCore;

namespace NetCoreVueJsPOC.DAL
{
    public sealed partial class DbInitializer
    {
        public static void Initialize(NetCoreVueJsPOCContext context)
        {
            context.Database.Migrate();

            LoadWeatherForecastSummaries(context);
            LoadWeatherForecasts(context);

            context.SaveChanges();
        }
    }
}
