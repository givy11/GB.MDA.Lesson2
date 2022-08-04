using GB.MDA.Lesson2.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();
var app = builder.Build();

app.Run();