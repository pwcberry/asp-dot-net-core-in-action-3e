using MyLearning.Chapter09.Email;
using Microsoft.AspNetCore.Mvc;
using MyLearning.Chapter09.Input;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddEmailSender();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/register", (IEmailSender emailSender, [FromBody] Registration form) =>
{
    emailSender.SendEmail(form.Email);
    return Results.Ok();
});

app.Run();
