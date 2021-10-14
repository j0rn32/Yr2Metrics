public interface IWeatherCache
{
    void Set(float? temp, float? windspeed, float? direction);
    (float? temp, float? windspeed, float? direction) Get();
}

public class WeatherCache : IWeatherCache
{
    private float? Temperature;
    private float? WindSpeed;
    private float? WindDirection;

    public void Set(float? temp, float? windspeed, float? direction) => (Temperature, WindSpeed, WindDirection) = (temp, windspeed, direction);
    public (float? temp, float? windspeed, float? direction) Get() => (Temperature, WindSpeed, WindDirection);
}