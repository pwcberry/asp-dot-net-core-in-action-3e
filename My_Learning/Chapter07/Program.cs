/// <summary>
/// Chapter 7: Model binding and validation in minimal APIs
/// </summary>
using Microsoft.AspNetCore.Mvc;
using MyLearning.Services;
using MyLearning.Services.Inputs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseService(MyLearningDatabase.Movieland);
var app = builder.Build();

app.MapGet("/", () => "Welcome to Movieland!");

app.MapGet("/movies/{id}", (MovielandService service, int id) =>
{
    var movie = service.GetMovie(id);
    return movie is not null ? Results.Ok(movie) : Results.NotFound();
});

app.MapGet("/movies", (MovielandService service, int id) =>
{
    var movie = service.GetMovie(id);
    return movie is not null ? Results.Ok(movie) : Results.NotFound();
});

app.MapPost("/user/rating", (MovielandService service, [FromHeader(Name = "User")] int userId, MovieRating rating) =>
{
    var result = service.AddRating(userId, rating.MovieId, rating.Rating);
    return result ? Results.Ok() : Results.BadRequest();
});

app.Run();

