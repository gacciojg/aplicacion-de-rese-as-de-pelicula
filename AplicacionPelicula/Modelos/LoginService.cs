using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AplicacionPelicula.Modelos;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace AplicacionPelicula.Modelos
{
    public class LoginService
    {
        private readonly List<Login> _usuarios = new List<Login>//lista de usuarios/admins
        {
            new Login { Username = "admin", Password = "12345678", Rol = "Administrador" },
            new Login { Username = "user", Password = "abcdefgh", Rol = "Usuario" }
        };

        public Login? UsuarioActual { get; private set; }

        public Login? validarUsuario(string username, string password)//valida las credenciales ingresadas
        {
            var user = _usuarios.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            UsuarioActual = user;//guarda el usuario actual

            return user;//retorna el usuario encontrado
        }

        public void Logout()//cierre sesion
        {
            UsuarioActual = null;
        }

        public bool EstaLogueado()//verifica si hay un usuario logueado
        {
            return UsuarioActual != null;
        }

        public string? ObtenerRol()//retornar el rol del usuario loguedado
        {
            return UsuarioActual?.Rol;
        }
        /*public Login? validarUsuario(string username, string password)
        {
            return _usuarios.FirstOrDefault(u =>
            u.Username == username && u.Password == password);
        }*/
    }
}
