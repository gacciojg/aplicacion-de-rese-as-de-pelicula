using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPelicula.Modelos
{
    public class Resenia
    {
        public int Id { get; set; }
        public int PeliculaId { get; set; }

        [StringLength(500)]
        public string? Comentario { get; set; }

        [Range(1, 5, ErrorMessage = "la calificacion debe estar entre 1 y 5 estrellas")]
        public int Estrellas { get; set; }//calificacion en estrellas
        public DateTime Fecha { get; set; } = DateTime.Now; 
        public Pelicula? Pelicula { get; set; }//objeto que permite acceder a los datos de pelicula
    }
}
