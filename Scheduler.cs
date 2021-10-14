using System.Threading;
using System.Threading.Tasks;

public class Scheduler : BackgroundService
{
    private readonly IYrApi YrApi;
    private readonly IWeatherCache WeatherCache;
    private readonly ILogger<Scheduler> Log;

    public Scheduler(IYrApi yrApi, IWeatherCache weatherCache, ILogger<Scheduler> log)
    {
        YrApi = yrApi;
        WeatherCache = weatherCache;
        Log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stopToken)
    {
        Log.LogInformation("Starting up scheduler...");
        while (!stopToken.IsCancellationRequested)
        {
            try
            {
                Log.LogInformation("Scheduler activated.");
                var weather = await YrApi.GetWeatherData();
                WeatherCache.Set(weather.temp, weather.windSpeed, weather.windDirection, weather.precipitation, weather.humidity, weather.windGust);
                await Task.Delay(TimeSpan.FromMinutes(15), stopToken);
            }
            catch (TaskCanceledException)
            {
                Log.LogInformation("Scheduler stopped");
            }
            catch (Exception ex)
            {
                Log.LogWarning(ex, "Scheduler failed");
                await Task.Delay(TimeSpan.FromMinutes(3), stopToken);
            }
        }
    }
}