using MyLearning.Chapter05;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var service = new Service();

app.MapTrackRouteGroup(service);
app.MapGet("/tracks", () => service.GetAllTracks());
app.MapGet("/artist/{name}", (string name) => service.FilterByArtist(name));

app.MapGet("/teapot", (HttpResponse response) =>
{
    response.StatusCode = 418;
    response.ContentType = MediaTypeNames.Text.Plain;
    return response.WriteAsync("I'm a TEAPOT!");
});

app.Run();
