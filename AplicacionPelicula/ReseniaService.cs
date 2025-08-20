using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AplicacionPelicula
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
                .Where(r => r.PeliculaId == peliculaId)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
        }

        public async Task AgregarReseñasAsync(Resenia reseña)
        {
            _context.Resenias.Add(reseña);
            await _context.SaveChangesAsync();
        }
    }
}
//asincronia = todos los metodos son asincronos, lo que es importante para operaciones 
//E/S como acceso a base de datos
