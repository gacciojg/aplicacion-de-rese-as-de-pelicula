using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPelicula.Modelos
{
    public class Usuario
    {
        public int Id {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol {  get; set; }
        public string ImagenRuta { get; set; }//ruta donde se guarda la imagen
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
