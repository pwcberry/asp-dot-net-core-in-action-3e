/// <summary>
/// Chapter 7: Model binding and validation in minimal APIs
/// </summary>
using Microsoft.AspNetCore.Mvc;
using MyLearning.Services;
using MyLearning.Services.Inputs;
using MyLearning.Chapter07;
using Movie = MyLearning.Data.Sqlite.Movieland.Movie;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseService(MyLearningDatabase.Movieland);
var app = builder.Build();

app.MapGet("/", () => "Welcome to Movieland!");

app.MapGet("/movies/{id}", (MovielandService service, int id) =>
{
    var movie = service.GetMovie(id);
    return movie is not null ? Results.Ok(movie) : Results.NotFound();
});

app.MapGet("/movies", (MovielandService service, [FromQuery(Name = "id")] int[] ids) =>
{
    var result = new List<Movie>();

    foreach(var id in ids)
    {
        var movie = service.GetMovie(id);
        if (movie is not null)
        {
            result.Add(movie);
        }
    }

    return result.Count > 0 ? Results.Ok(result) : Results.NotFound();
});

app.MapGet("/movies/search", (MovielandService service, [AsParameters] MovieSearch search) =>
{
    var result = service.SearchMovies(search.Query, search.Page);
    return result.Count > 0 ? Results.Ok(result) : Results.NotFound();
});

app.MapPost("/user/rating", (MovielandService service, [FromHeader(Name = "User")] int userId, MovieRating rating) =>
{
    var result = service.AddRating(userId, rating.MovieId, rating.Rating);
    return result ? Results.Ok() : Results.BadRequest();
});

app.MapPost("/user/rating/custom", (MovielandService service, MovieRatingCustom rating) =>
{
    var result = service.AddRating(rating.UserId, rating.MovieId, rating.Rating);
    return result ? Results.Ok() : Results.BadRequest();
});

app.Run();

