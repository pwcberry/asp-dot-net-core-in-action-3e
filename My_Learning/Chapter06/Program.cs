using MyLearning.Chapter06.Services;
using MyLearning.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// When running the app, specify the SQLite database path in the environment variable DB_PATH.
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddScoped<NorthwindService>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("OK"));

app.MapGet("/employees", (NorthwindService service) => service.GetEmployees());
app.MapGet("/employee/{id:int}", (NorthwindService service, int id) => service.GetEmployee(id));
app.MapGet("/categories", (NorthwindService service) => service.GetCategories());
app.MapGet("/territories/{name}/orders", (NorthwindService service, string name) =>
{
    var territory = service.GetTerritoryByName(name);
    if (territory is null)
        return Results.NotFound();

    var orders = service.GetOrdersByTerritory(territory.Id);
    return Results.Ok(orders);
}).WithName("orders-by-territories");

app.MapGet("/redirect", (LinkGenerator linkGen) =>
{
    // If the mapped route above isn't given an Endpoint name (using the method "WithName")
    // then the link generator will return null as it's not able to find the route by name.
    var url = linkGen.GetPathByName("orders-by-territories",  new { name = "new york" });
    return Results.Redirect(url!);
});

app.MapGet("/territory/{name}", (string name) => Results.RedirectToRoute("orders-by-territories", new { name }, permanent: true, preserveMethod: true));

app.Run();
