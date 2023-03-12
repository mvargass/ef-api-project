using entityapi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var dir = $"{Directory.GetCurrentDirectory()}\\Config\\";
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile(dir + "appsettings.json",
                       optional: true,
                       reloadOnChange: true);
});
//builder.Services.AddDbContext<TareasContext>(opt => opt.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("TareaConn"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext context) => {
    context.Database.EnsureCreated();
    return Results.Ok($"Base de datos en sql server {context.Database.IsInMemory()}");
});

app.Run();
