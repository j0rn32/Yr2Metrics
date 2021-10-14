public interface IWeatherCache
{
    void Set(float? temp, float? windSpeed, float? windDirection, float? precipitation, float? humidity, float? windGust);
    (float? temp, float? windSpeed, float? windDirection, float? precipitation, float? humidity, float? windGust) Get();
}

public class WeatherCache : IWeatherCache
{
    private float? Temperature;
    private float? WindSpeed;
    private float? WindDirection;
    private float? Precipitation;
    private float? Humidity;
    private float? WindSpeedGust;

    public void Set(float? temp, float? windSpeed, float? windDirection, float? precipitation, float? humidity, float? windGust) =>
        (Temperature, WindSpeed, WindDirection, Precipitation, Humidity, WindSpeedGust) = (temp, windSpeed, windDirection, precipitation, humidity, windGust);

    public (float? temp, float? windSpeed, float? windDirection, float? precipitation, float? humidity, float? windGust) Get() =>
        (Temperature, WindSpeed, WindDirection, Precipitation, Humidity, WindSpeedGust);
}