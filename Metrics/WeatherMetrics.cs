public interface IWeatherMetrics
{
    string Generate();
}

public class WeatherMetrics : IWeatherMetrics
{
    private readonly IWeatherCache WeatherCache;
    private readonly ILogger<WeatherMetrics> Log;

    public WeatherMetrics(IWeatherCache weatherCache, ILogger<WeatherMetrics> log)
    {
        WeatherCache = weatherCache;
        Log = log;
    }

    public string Generate()
    {
        Log.LogInformation("Generating Metrics");
        var (Temperature, WindSpeed, WindDirection) = WeatherCache.Get();
        var sb = new StringBuilder();
        sb.AppendLine($"Temperature {Temperature}");
        sb.AppendLine($"WindSpeed {WindSpeed}");
        sb.AppendLine($"WindDirection {WindDirection}");
        return sb.ToString();
    }
}