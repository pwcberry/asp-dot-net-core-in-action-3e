using MyLearning.Chapter04;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.SetupMinimalApi();
//app.SetupDeveloperExceptionHandling();
app.SetupExceptionHandling();
app.Run();
