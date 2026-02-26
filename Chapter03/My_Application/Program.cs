using MyApplication;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world!");

app.MapGet("/sum", (int a, int b) => $"{a} + {b} = {(a + b)}");

app.MapGet("/person", (string name, int age) => new Person(name, age).ToString());

app.MapPost("/person", (Person p) => p.ToString());

app.Run();


