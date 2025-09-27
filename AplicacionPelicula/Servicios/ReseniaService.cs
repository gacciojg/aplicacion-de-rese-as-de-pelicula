using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacionPelicula.BaseDatos;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AplicacionPelicula.Servicios
{
    public class ReseniaService
    {
        private readonly AppDbContext _context; //dbcontext inyectado

        public ReseniaService(AppDbContext context)//constructor que recibe el dbcontext por inyeccion
        {
            _context = context;
        }

        public async Task<List<Resenia>> GetReseñaByPeliculaIdsAsync(int peliculaId)
        {
            return await _context.Resenias
                .Where(r => r.PeliculaId == peliculaId)//filtrado de reseñas y trae solo las que pertenecen a la pelicula indicada
                .OrderByDescending(r => r.Fecha)//aparece la reseña mas nueva
                .ToListAsync();
        }

        public async Task AgregarReseñasAsync(Resenia reseña)
        {
            _context.Resenias.Add(reseña);
            await _context.SaveChangesAsync();//"escribe"en la base de datos
        }
    }
}
//asincronia = todos los metodos son asincronos, lo que es importante para operaciones 
//E/S como acceso a base de datos
