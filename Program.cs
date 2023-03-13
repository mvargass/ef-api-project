using entityapi;
using entityapi.Models;
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

app.MapGet("/api/tareas", async([FromServices] TareasContext context) => 
{
    return Results.Ok(context.Tareas.Include(t=>t.Categoria));
});

app.MapGet("/api/tareas/{id}", async([FromServices] TareasContext context, [FromRoute] Guid id) => 
{
    return Results.Ok(context.Tareas.FirstOrDefault(t => t.TareaId == id));
});

app.MapGet("/api/tareas/categoria/{catId}", async([FromServices] TareasContext context, Guid catId) => 
{
    return Results.Ok(context.Tareas.Include(t=> t.Categoria).Where(t => t.CategoriaId == catId));
});

app.MapGet("/api/tareas/prioridad/{prioridad}", async([FromServices] TareasContext context, Prioridad prioridad) => 
{
    return Results.Ok(context.Tareas.Where(t=>t.PrioridadTarea == prioridad));
});

app.MapPost("/api/tareas", async([FromServices] TareasContext context, [FromBody] Tarea tarea) => 
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await context.Tareas.AddAsync(tarea);
    
    if (context.SaveChanges() > 0)
    {
        return Results.Ok("Registro insertado");
    } else {
        return Results.BadRequest("Error al insertar la tarea");
    }
});

app.MapPut("/api/tareas/{id}", async([FromServices] TareasContext context, [FromRoute] Guid id, [FromBody] Tarea tarea) => {
    var tareaActual = context.Tareas.Find(id);
    if (tareaActual == null){
        return Results.BadRequest($"No existe ninguna tarea con el id {id}");
    } else {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Descripcion = tarea.Descripcion;
        tareaActual.Estatus = tarea.Estatus;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Titulo = tarea.Titulo; 

        if (context.SaveChanges() > 0){
            return Results.Ok("Tarea actualizada exitosamente");
        } else {
            return Results.BadRequest("Error al actualizar la tarea");
        }
    }
});

app.MapDelete("/api/tareas/{id}", async([FromServices] TareasContext context, [FromRoute] Guid id) => {
    var tareaActual = context.Tareas.Find(id);
    if (tareaActual == null){
        return Results.NotFound($"No existe una tarea con el id {id}");
    } else {
        context.Tareas.Remove(tareaActual);
        if (context.SaveChanges() > 0){
            return Results.Ok("Tarea eliminada exitosamente");
        } else {
            return Results.NotFound("Error al eliminar la tarea");
        }
    }
});

app.MapGet("api/categorias", async([FromServices] TareasContext context) => 
{
    return Results.Ok(context.Categorias);
});

app.Run();
