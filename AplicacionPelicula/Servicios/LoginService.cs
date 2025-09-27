using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AplicacionPelicula.Modelos;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using System.Net.Http.Json;

namespace AplicacionPelicula.Servicios
{
    public class LoginService
    {
        private readonly HttpClient _http;

        public Login? UsuarioActual { get; private set; }//login? permite consultar estado  de sesion y permisos

        public LoginService(HttpClient http) {
            _http = http;
        }

        public async Task<(bool Exito, string Mensaje, Login? Usuario)> validarUsuario(string username, string password)//valida las credenciales ingresadas
        {
            var loginDTO = new { Username = username, Password = password };
            var response = await _http.PostAsJsonAsync("https://localhost:7026/api/Login/login", loginDTO);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return (false, $"error de login: {err}", null);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result == null) return (false, "no se pudo procesar la respuesta del servidor", null);

            var usuario = new Login
            {
                Id = 0,
                Username = result.Username,
                Password = password,
                Rol = result.Rol
            };

            UsuarioActual = usuario;
            return (true, result.Message, usuario);
        }

        public async Task<(bool Exito, string Mensaje)> RegistrarUsuario(string username, string password, string rol = "usuario")
        {
            var nuevo = new 
            {
                Username = username,
                Password = password,
                Rol = rol
            };

            var response = await _http.PostAsJsonAsync("https://localhost:7026/api/Login/registrar", nuevo);

            if (response.IsSuccessStatusCode)
            {
                var mensaje = await response.Content.ReadAsStringAsync();//si la api devuelve un mensaje tipo string/json
                return (true, mensaje);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, $"error al registrar: {error}");
            }

                return (true, "usuario registrado con exito");
        }

        public void Logout() => UsuarioActual = null; //cierre sesion

        public bool EstaLogueado() => UsuarioActual != null; //verifica si hay un usuario logueado

        public string? ObtenerRol() => UsuarioActual?.Rol;//retornar el rol del usuario loguedado

        public int? ObtenerIdUsuario() => UsuarioActual?.Id;//devuelve el identificador unico del usuario autenticado
    }

    public class LoginResponse//clase auxiliar para mapear la respuesta del login
    {
        public string Message { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
