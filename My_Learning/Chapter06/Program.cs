using MyLearning.Chapter06.Services;
using MyLearning.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// When running the app, specify the SQLite database path in the environment variable DB_PATH.
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddScoped<NorthwindService>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("OK"));


app.MapGet("/categories", (NorthwindService service) => service.GetCategories());

app.Run();
