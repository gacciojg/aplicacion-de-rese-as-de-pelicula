using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPelicula.Modelos
{
    public class Pelicula
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "el titulo no puede superar los 50 caracteres")]
        public string Titulo { get; set; } = string.Empty;//<- evito problemas al serializar o mapear

        [Required(ErrorMessage = "el genero es obligatorio")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "la descripcion es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "el año debe estar entre 1900 y 2100")]
        public int? Anio { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow; //fecha, hora actual

        [Required(ErrorMessage = "la ruta de la imagen es obligatoria")]
        public string ImagenRuta { get; set; } = string.Empty;
   
        public byte[]? ImagenBytes { get; set; }//bytes de la imagen para mostrarla como Base64

        public List<Resenia> Resenias { get; set; } = new List<Resenia>();//relacion uno a muchos, evito el nullreferenceexception
    }
}
