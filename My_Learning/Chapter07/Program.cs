/// <summary>
/// Chapter 7: Model binding and validation in minimal APIs
/// </summary>
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
