using System.Diagnostics;
using AdamTibi.OpenWeather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

string[] summaries =
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

string MapFeelToTemp(int temperatureC)
{
    if (temperatureC <= 0) return summaries.First();
    int summariesIndex = (temperatureC / 5) + 1;
    if (summariesIndex >= summaries.Length) return summaries.Last();
    return summaries[summariesIndex];
}


app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


int FORECAST_DAYS = 5;

app.MapGet("/GetRandomWeatherForecast", () =>
{
    WeatherForecast[] wfs = new WeatherForecast[FORECAST_DAYS];
    for (int i = 0; i < wfs.Length; i++)
    {
        var temperatureC = Random.Shared.Next(-20, 55);
        wfs[i] = new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(i + 1)), temperatureC, MapFeelToTemp(temperatureC));
    }
    return wfs;
});

app.MapGet("/GetRealWeatherForecast", async () =>
{
    // Get the value of OpenWeather.Key from appsettings.Devvelopment.json    .GetValue<string>();
    IConfiguration _config = app.Configuration;
    string apiKey = _config["OpenWeather:Key"]!;

    Console.WriteLine(apiKey);

    var httpClient = new HttpClient();
    //  make a get request to this URL: https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}
    const decimal latitude = 51.48m;
    const decimal longitude = 0.00m;
    string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
    Console.WriteLine(url);

    HttpResponseMessage response = await httpClient.GetAsync(url);
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    return Results.Content(responseBody, "application/json");
});

app.MapGet("ConvertCToF", (double c) =>
{
    double f = c * (9d / 5d) + 32;
    // Please add logger loginformation here
    app.Logger.LogInformation("Conversion from C to F has been made successfully!");
    return f;
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}