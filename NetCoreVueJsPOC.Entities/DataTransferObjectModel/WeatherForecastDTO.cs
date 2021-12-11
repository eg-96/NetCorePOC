namespace NetCoreVueJsPOC.Entities.DataTransferObjectModel
{
    public class WeatherForecastDTO : WeatherForecastUpdateDTO
    {
        public string SummaryDesc { get => Summary.Label; }
    }
}
