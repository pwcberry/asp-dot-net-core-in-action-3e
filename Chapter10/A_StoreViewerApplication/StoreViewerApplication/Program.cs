using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<MapSettings>()
    .BindConfiguration("MapSettings");
builder.Services.AddOptions<AppDisplaySettings>()
    .BindConfiguration("AppDisplaySettings");
builder.Services.AddOptions<List<Store>>()
    .BindConfiguration("Stores");

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/stores", (IOptions<List<Store>> opts) => opts.Value);
app.MapGet("/map-settings", (IOptions<MapSettings> opts) => opts.Value);
app.MapGet("/display-settings", (IOptions<AppDisplaySettings> opts) => opts.Value);

// Don't favour this approach
app.MapGet("/display-settings-alt", (IConfiguration config) => new
{
    title = config["AppDisplaySettings:Title"],
    showCopyright = bool.Parse(
        config["AppDisplaySettings:ShowCopyright"]!),
});

app.Run();

class MapSettings
{
    public string GoogleMapsApiKey { get; set; }
    public int DefaultZoomLevel { get; set; }
    public Location DefaultLocation { get; set; }
}
class Location
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}
class Store
{
    public string Name { get; set; }
    public Location Location { get; set; }
}
class AppDisplaySettings
{
    public string Title { get; set; }
    public bool ShowCopyright { get; set; }
}