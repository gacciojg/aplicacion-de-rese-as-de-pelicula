using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionPelicula.BaseDatos;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace AplicacionPelicula.Servicios
{
    public class PeliculaService
    {
        private readonly AppDbContext _context; //instancia del contexto que maneja la db

        public PeliculaService(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); //crea la base de datos y tablas si no existen
        }

        public async Task AgregarPeliculaAsync(Pelicula pelicula)//inserta un nuevo registro en la tabla peliculas
        {
            try
            {
                _context.Peliculas.Add(pelicula);
                await _context.SaveChangesAsync();//guarda cambios
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
                ImagenBytes = bytesImagen,//guarda la imagen en binario
                ImagenRuta = rutaArchivo 
            };

            await AgregarPeliculaAsync(pelicula);
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()//retornta una lista completa de peliculas
        {
            return await _context.Peliculas.ToListAsync();
        }

        public async Task<Pelicula> GetPeliculaByIdAsync(int id)//obtener un solo movimiento por el id
        {
            return await _context.Peliculas
                .AsNoTracking()//evita que no guarde en su lista de entidades rastreadas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task ActualizarPeliculaAsync(Pelicula pelicula)//marca el objeto como modificado y guarda los cambios
        {
            var entity = await _context.Peliculas.FindAsync(pelicula.Id);
            if(entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(pelicula);//busca la entidad original en la bd y aplica los cambios del clon, evitando conflictos con tracking
                await _context.SaveChangesAsync();
            }
        }

        public async Task BorrarPeliculaAsync(int id)
        {
            var Pelicula = await _context.Peliculas.FindAsync(id);//busca el registro
            if (Pelicula != null)
            {
                _context.Peliculas.Remove(Pelicula);//elimina
                await _context.SaveChangesAsync();//guarda cambios
            }
        }
    }
}
