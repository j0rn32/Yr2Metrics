var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IYrApi, YrApi>();
builder.Services.AddSingleton<IWeatherCache, WeatherCache>();
builder.Services.AddSingleton<IWeatherMetrics, WeatherMetrics>();
builder.Services.AddHostedService<Scheduler>();
builder.Services.AddSingleton<YrConfig>(builder.Configuration.GetSection("Yr").Get<YrConfig>());
var app = builder.Build();
app.UseDeveloperExceptionPage();
app.MapGet("/metrics", Metrics);

IResult Metrics(IWeatherMetrics weatherMetrics) => Results.Text(weatherMetrics.Generate());

await app.RunAsync();