var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "API da biblioteca está no ar!");

app.Run();