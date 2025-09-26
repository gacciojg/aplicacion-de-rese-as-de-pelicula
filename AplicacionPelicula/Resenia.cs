using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPelicula
{
    public class Resenia
    {
        public int Id { get; set; }
        public int PeliculaId { get; set; }
        public string? Comentario { get; set; }
        public bool MeGusta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now; 
        public Pelicula? Pelicula { get; set; }
    }
}
