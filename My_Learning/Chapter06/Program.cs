using MyLearning.Chapter06.Services;
using MyLearning.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddScoped<NorthwindService>();
var app = builder.Build();

app.MapGet("/", () => Results.Ok("OK"));

app.MapGet("/categories", (NorthwindService service) => service.GetCategories());

app.Run();
