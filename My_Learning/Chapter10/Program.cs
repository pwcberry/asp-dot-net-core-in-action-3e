var builder = WebApplication.CreateBuilder(args);
// builder.Configuration.Sources.Clear();
// builder.Configuration
//     .AddJsonFile("appsettings.json", optional: false)
//     .AddJsonFile("pete-settings.json", optional: true)
//     .AddUserSecrets(typeof(Program).Assembly, optional: true);
builder.Configuration.AddJsonFile("pete-settings.json", optional: true, reloadOnChange: true);

var app = builder.Build();

app.MapGet("/", (IConfiguration config) => config.GetSection("Preferences").AsEnumerable());

app.Run();
