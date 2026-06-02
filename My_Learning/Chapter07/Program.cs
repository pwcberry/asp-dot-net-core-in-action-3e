/// <summary>
/// Chapter 7: Model binding and validation in minimal APIs
/// </summary>
using MyLearning.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseService(MyLearningDatabase.Northwind);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
