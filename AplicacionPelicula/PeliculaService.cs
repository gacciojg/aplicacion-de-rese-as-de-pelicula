 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace AplicacionPelicula
{
    public class PeliculaService
    {
        private readonly AppDbContext _context; //instancia del contexto que maneja la db

        public PeliculaService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated(); //crea la base de datos y tablas si no existen
        }

        public async Task AgregarPeliculaAsync(Pelicula pelicula)//inserta un nuevo registro en la tabla peliculas
        {
            try
            {
                using var context = new AppDbContext();
                context.Peliculas.Add(pelicula);
                await context.SaveChangesAsync();//guarda cambios
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message; //captura la excepcion especifica de ef core
                throw new Exception($"error guardando la pelicula: {innerMessage}");//mas informacion
            }
            catch (Exception ex)
            {
                throw new Exception($"error inesperado: {ex.Message}");
            }
        }

        public async Task AgregarPeliculaDesdeArchivoAsync(string rutaArchivo, string titulo, string genero, string descripcion, double anio)//crea y agrega pelicula desde una ruta de archivo
        {
            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException("No se encontró la imagen.", rutaArchivo);

            var bytesImagen = File.ReadAllBytes(rutaArchivo);

            var pelicula = new Pelicula
            {
                Titulo = titulo,
                Genero = genero,
                Descripcion = descripcion,
                Anio = anio,
                Fecha = DateTime.Now,
                ImagenBytes = bytesImagen,
                ImagenRuta = rutaArchivo 
            };

            await AgregarPeliculaAsync(pelicula);
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()//retornta una lista completa de peliculas
        {
            using var context = new AppDbContext();
            return await context.Peliculas.ToListAsync();
        }

        public async Task<Pelicula> GetPeliculaByIdAsync(int id)//obtener un solo movimiento por el id
        {
            using var context = new AppDbContext();
            return await context.Peliculas
                .AsNoTracking()//evita que no guarde en su lista de entidades rastreadas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task ActualizarPeliculaAsync(Pelicula pelicula)//marca el objeto como modificado y guarda los cambios
        {
            using var context = new AppDbContext();
            context.Peliculas.Update(pelicula);
            await context.SaveChangesAsync();
        }

        public async Task BorrarPeliculaAsync(int id)
        {
            using var context = new AppDbContext();
            var Pelicula = await context.Peliculas.FindAsync(id);//busca el registro
            if (Pelicula != null)
            {
                context.Peliculas.Remove(Pelicula);//elimina
                await context.SaveChangesAsync();//guarda cambios
            }
        }
    }
}
