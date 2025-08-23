using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPelicula
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Genero { get; set; }
        public string? Descripcion { get; set; }

        public double? Anio { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now; //fecha, hora actual
        public string? ImagenRuta { get; set; }
   
        public byte[]? ImagenBytes { get; set; }//bytes de la imagen para mostrarla como Base64

        public List<Resenia> Resenias { get; set; } = new List<Resenia>();
    }
}
