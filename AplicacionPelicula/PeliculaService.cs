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

        //inserta un nuevo registro en la tabla peliculas
        public async Task AgregarPeliculaAsync(Pelicula pelicula)
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

        //retornta una lista completa de peliculas
        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            using var context = new AppDbContext();
            return await context.Peliculas.ToListAsync();
        }

        //obtener un solo movimiento por el id
        public async Task<Pelicula> GetPeliculaByIdAsync(int id)
        {
            using var context = new AppDbContext();
            return await context.Peliculas
                .AsNoTracking()//evita que no guarde en su lista de entidades rastreadas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //marca el objeto como modificado y guarda los cambios
        public async Task ActualizarPeliculaAsync(Pelicula pelicula)
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
//si quiero realizar los ejercicios que me faltan deberia empezar por crear otra base de datos aparte para el usuario
//asi cuando cree el login, se registrara en una lista aparte el usuario que se logueo, entonces, para completar
//el tema maestro-detalle, debo crear un servicio nuevo, en este caso, el usuario, una lista aparte y una base de datos aparte, cuando diseñe el login
//se hara mas evidente esto, pudiendo acceder a una lista aparte donde estaran los usuarios registrados
//y a su vez, podre continuar con la pagina principal de peliculas. nota mental
//crea el menu primero antes de empezar con el usuario, login, etc.
//los iconos son solo iconos que resaltan la utilidad de ciertos botones, y podes agrgar la imagen que quieras
//menu -> basededatos -> servicioUsuario -> login 