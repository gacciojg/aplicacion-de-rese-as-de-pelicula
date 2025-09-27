using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore; //importa ef core para trabajar con dbcontext y dbset
using Microsoft.Maui.Storage;

namespace AplicacionPelicula.BaseDatos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)//hereda de dbcontext y recibe en eñ constructor las opciones de configuracion
    {
    }

    public DbSet<Pelicula> Peliculas { get; set; }//representa todas las peliculas en la base de datos
    public DbSet<Resenia> Resenias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pelicula>(entity =>//configuracion explicita de las tablas
        {
            entity.ToTable("Peliculas");//nombre de tabla
            entity.HasKey(p => p.Id);//clave primaria

            entity.HasMany(p => p.Resenias)//relacion 1 a N
                  .WithOne(r => r.Pelicula)
                  .HasForeignKey(r => r.PeliculaId);
        });

        modelBuilder.Entity<Resenia>(entity =>
        {
            entity.ToTable("Resenias");
            entity.HasKey(r => r.Id);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Username).IsUnique();//username unico
        });
    }
}
