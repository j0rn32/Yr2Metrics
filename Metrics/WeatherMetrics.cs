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
        var weather = WeatherCache.Get();
        var sb = new StringBuilder();
        sb.AppendLine($"Temperature {weather.temp}");
        sb.AppendLine($"WindSpeed {weather.windSpeed}");
        sb.AppendLine($"WindDirection {weather.windDirection}");
        sb.AppendLine($"WindSpeedGust {weather.windGust}");
        sb.AppendLine($"Precipitation {weather.precipitation}");
        sb.AppendLine($"Humidity {weather.humidity}");
        return sb.ToString();
    }
}