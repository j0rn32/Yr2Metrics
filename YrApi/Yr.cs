public interface IYrApi
{
    Task<(float? temp, float? windspeed, float? direction)> GetWeatherData(float latitude = 59.13f, float longitude = 10.16f);
}

public class YrApi : IYrApi
{
    private readonly ILogger<YrApi> Log;
    private readonly string BaseUrl = "https://api.met.no/weatherapi/nowcast/2.0/complete?"; //  lat=59&lon=10";
    private readonly HttpClient Client;

    private readonly (float lat, float lon) Location;

    public YrApi(ILogger<YrApi> log, YrConfig config)
    {
        Log = log;
        Log.LogInformation("YrApi using for {lat},{lon} & {userAgent}", config.Latitude, config.Longitude, config.UserAgent);
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.All,
            AllowAutoRedirect = true
        };
        Client = new HttpClient(handler);
        Client.DefaultRequestHeaders.Add("user-agent", config.UserAgent);
        Location = (config.Latitude, config.Longitude);
    }

    public async Task<(float? temp, float? windspeed, float? direction)> GetWeatherData(float latitude = 59.13f, float longitude = 10.16f)
    {
        var url = BaseUrl + $"lat={Location.lat:0.00}&lon={Location.lon:0.00}";

        Log.LogInformation("Fetching {url} from Yr.", url);

        var webResponse = await Client.GetAsync(url);

        if (webResponse.StatusCode == HttpStatusCode.NotFound) // 404 NotFound means no data aviable
            return (0, 0, 0);
        if (webResponse.StatusCode == HttpStatusCode.PreconditionFailed) // 412 No time series found for this combination of parameters, check /observations/availableTimeSeries for more information.
            return (0, 0, 0);

        if (webResponse.StatusCode != HttpStatusCode.OK)
            return (0, 0, 0);

        var response = await webResponse.Content.ReadAsStringAsync();

        var frost = JsonSerializer.Deserialize<NowCastObservation>(response);
        var r = frost.properties.timeseries.First().data.instant.details;
        return (r.air_temperature, r.wind_speed, r.wind_from_direction);
    }
}