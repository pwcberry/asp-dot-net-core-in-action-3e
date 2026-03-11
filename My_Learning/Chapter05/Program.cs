using MyLearning.Chapter05;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var service = new Service();

app.MapGet("/tracks", () => service.GetAllTracks());
app.MapGet("/track/{id}", (int id) =>
{
    var data = service.GetTrack(id);
    return data is not null ? Results.Ok(data) : Results.NotFound();
});
app.MapPost("/track", (Track track) =>
{
    var id = service.AddTrack(track);
    return Results.Created($"/track/{id}", track);
});
app.MapPut("/track/{id}", (int id, Track track) =>
{
    bool result = service.UpdateTrack(id, track);
    return result ? Results.NoContent() : Results.NotFound();
});
app.MapDelete("/track/{id}", (int id) =>
{
    bool result = service.DeleteTrack(id);
    return result ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/artist/{name}", (string name) => service.FilterByArtist(name));

app.MapGet("/teapot", (HttpResponse response) =>
{
    response.StatusCode = 418;
    response.ContentType = MediaTypeNames.Text.Plain;
    return response.WriteAsync("I'm a TEAPOT!");
});

app.Run();
