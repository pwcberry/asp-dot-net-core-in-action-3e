using Microsoft.AspNetCore.HttpLogging;
using MyApplication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.MapGet("/", () => "Hello world!");
app.MapGet("/sum", (int a, int b) => $"{a} + {b} = {(a + b)}");
app.MapGet("/person", (string name, int age) => new Person(name, age));
app.MapPost("/person", (Person p) => p.ToString());

app.Run();
