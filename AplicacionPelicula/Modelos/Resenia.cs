using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPelicula.Modelos
{
    public class Resenia
    {
        public int Id { get; set; }
        public int PeliculaId { get; set; }
        public string? Comentario { get; set; }
        public int Estrellas { get; set; }//calificacion en estrellas
        public DateTime Fecha { get; set; } = DateTime.Now; 
        public Pelicula? Pelicula { get; set; }//objeto que permite acceder a los datos de pelicula
    }
}
