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
            new Login { Id = 1, Username = "admin", Password = "12345678", Rol = "Administrador" },
            new Login { Id = 2, Username = "user", Password = "abcdefgh", Rol = "Usuario" }
        };

        private readonly UsuarioService  _usuarioService;

        public Login? UsuarioActual { get; private set; }

        public LoginService(UsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        public async Task<Login?> validarUsuario(string username, string password)//valida las credenciales ingresadas
        {
            var user = _usuarios.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (user != null)
            {
                UsuarioActual = user;

                bool existe = await _usuarioService.UsuarioExisteAsync(user.Username);
                if (!existe)
                {
                    var nuevoUsuario = new Usuario
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        Rol = user.Rol,
                        ImagenRuta = string.Empty,
                        FechaRegistro = DateTime.Now
                    };
                    await _usuarioService.AgregarUsuarioAsync(nuevoUsuario);
                }
            }
            return user;//retorna el usuario encontrado
        }

        public void Logout() => UsuarioActual = null; //cierre sesion

        public bool EstaLogueado() => UsuarioActual != null; //verifica si hay un usuario logueado

        public string? ObtenerRol() => UsuarioActual?.Rol;//retornar el rol del usuario loguedado

        public int? ObtenerIdUsuario() => UsuarioActual?.Id;
    }
}
