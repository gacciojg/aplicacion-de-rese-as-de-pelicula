using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AplicacionPelicula.BaseDatos;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace AplicacionPelicula.Servicios
{
    public class PeliculaService
    {
        private readonly HttpClient _http;

        public PeliculaService() 
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7026/")
            };
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            try
            {
                var response = await _http.GetAsync("https://localhost:7026/api/Pelicula/lista");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Pelicula>>();
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"error http: {ex.Message}");
                return new List<Pelicula>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error general: {ex.Message}");
                return new List<Pelicula>();
            }
        }

        public async Task<Pelicula?> GetPeliculaByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Pelicula>($"https://localhost:7026/api/Pelicula/buscar/{id}");
        }

        public async Task AgregarPeliculaAsync(Pelicula pelicula)//inserta un nuevo registro en la tabla peliculas
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7026/api/Pelicula/guardar", pelicula);
            response.EnsureSuccessStatusCode();
        }

        public async Task ActualizarPeliculaAsync(Pelicula pelicula)//marca el objeto como modificado y guarda los cambios
        {
            var response = await _http.PutAsJsonAsync("https://localhost:7026/api/Pelicula/editar", pelicula);
            response.EnsureSuccessStatusCode();
        }

        public async Task BorrarPeliculaAsync(int id)
        {
            var response = await _http.DeleteAsync($"https://localhost:7026/api/Pelicula/eliminar/{id}");
        }
    }
}
