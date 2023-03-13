using System.Reflection.Emit;
using entityapi.Models;
using Microsoft.EntityFrameworkCore;

namespace entityapi;
public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Tarea> Tareas {get; set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base(options){ }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        List<Tarea> tareasInit = new List<Tarea>();
        categoriasInit.Add(new Categoria() 
        {
            CategoriaId = Guid.Parse("771167b7-6e0b-429c-85ad-de56cddab0c4"),
            Nombre = "Actividades Pendientes",
            Peso = 20
        });

        categoriasInit.Add(new Categoria() 
        {
            CategoriaId = Guid.Parse("771167b7-6e0b-429c-85ad-de56cddab002"),
            Nombre = "Actividades Personales",
            Peso = 50
        });

        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("771167b7-6e0b-429c-85ad-de56cddab010"),
            CategoriaId = Guid.Parse("771167b7-6e0b-429c-85ad-de56cddab0c4"),
            PrioridadTarea = Prioridad.Media,
            Titulo = "Pago de Servicios Públicos",
            FechaCreacion = DateTime.Now,
            Estatus = Estatus.Pendiente
        });

        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("771167b7-6e0b-429c-85ad-de56cddab011"),
            CategoriaId = Guid.Parse("771167b7-6e0b-429c-85ad-de56cddab002"),
            PrioridadTarea = Prioridad.Baja,
            Titulo = "Terminar de ver pelicula en Netflix",
            FechaCreacion = DateTime.Now,
            Estatus = Estatus.EnProceso
        });

        modelBuilder.Entity<Categoria>(c => {
            c.ToTable("Categoria");
            c.HasKey(k=> k.CategoriaId);
            c.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);
            c.Property(p=> p.Descripcion).IsRequired(false);
            c.Property(p=> p.Peso);

            c.HasData(categoriasInit);
        });

        modelBuilder.Entity<Tarea>(t => {
            t.ToTable("Tarea");
            t.HasKey(p=>p.TareaId);
            t.HasOne(p=>p.Categoria).WithMany(c=>c.Tareas).HasForeignKey(p=>p.CategoriaId);
            t.Property(p=>p.Titulo).IsRequired().HasMaxLength(200);
            t.Property(p=>p.Descripcion).IsRequired(false);
            t.Property(p=>p.FechaCreacion);
            t.Property(p=> p.PrioridadTarea);
            t.Property(p=>p.Estatus).HasDefaultValue(Estatus.Pendiente);
            t.Ignore(p=>p.Resumen);
            t.HasData(tareasInit);
        });
    }
}