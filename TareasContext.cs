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
        modelBuilder.Entity<Categoria>(c => {
            c.ToTable("Categoria");
            c.HasKey(k=> k.CategoriaId);
            c.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);
            c.Property(p=> p.Descripcion).IsRequired(false);

        });

        modelBuilder.Entity<Tarea>(t => {
            t.ToTable("Tarea");
            t.HasKey(p=>p.TareaId);
            t.HasOne(p=>p.Categoria).WithMany(c=>c.Tareas).HasForeignKey(p=>p.CategoriaId);
            t.Property(p=>p.Titulo).IsRequired().HasMaxLength(200);
            t.Property(p=>p.Descripcion).IsRequired(false);
            t.Property(p=>p.FechaCreacion);
            t.Property(p=> p.PrioridadTarea);
            t.Ignore(p=>p.Resumen);
        });
    }
}