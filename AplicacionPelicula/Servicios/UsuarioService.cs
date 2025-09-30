using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AplicacionPelicula.BaseDatos;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AplicacionPelicula.Servicios
{
    public class UsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Usuario>> ObtenerTodosUsuariosAsync()
        {
            try
            {
                var usuarios = await _http.GetFromJsonAsync<List<Usuario>>("https://localhost:7026/api/Usuario/lista");
                return usuarios ?? new List<Usuario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
                return new List<Usuario>();
            }
        }

        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Usuario>($"https://localhost:7026/api/Usuario/buscar/{id}");
        }

        public async Task<bool> CrearUsuarioAsync(Usuario usuario)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7026/api/Usuario/guardar", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditarUsuarioAsync(Usuario usuario)
        {
            var response = await _http.PutAsJsonAsync("https://localhost:7026/api/Usuario/editar", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var response = await _http.DeleteAsync($"https://localhost:7026/api/Usuario/eliminar/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
