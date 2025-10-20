using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AplicacionPelicula.BaseDatos;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AplicacionPelicula.Servicios
{
    public class ReseniaService
    {
        private readonly HttpClient _http;

        public ReseniaService(HttpClient http)//constructor que recibe el dbcontext por inyeccion
        {
            _http = http;
        }

        public async Task<List<Resenia>> GetReseniasPorPeliculasAsync(int peliculaId)
        {
            try
            {
                var resenias = await _http.GetFromJsonAsync<List<Resenia>>($"api/Resenia/lista/{peliculaId}");
                return resenias ?? new List<Resenia>();
            }catch(HttpRequestException ex)
            {
                Console.WriteLine($"error al obtener reseñas: {ex.Message}");
                return new List<Resenia>();
            }
        }

        public async Task<Resenia?> GetReseniaByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Resenia>($"api/Resenia/buscar/{id}");
        }

        public async Task AgregarReseniasAsync(Resenia resenia)
        {
            var response = await _http.PostAsJsonAsync("api/Resenia/guardar", resenia);
            response.EnsureSuccessStatusCode();
        }

        public async Task ActualizarReseniaAsync(Resenia resenia)
        {
            var response = await _http.PutAsJsonAsync("api/Resenia/editar", resenia);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> BorrarReseniaAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Resenia/eliminar/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }catch (HttpRequestException ex)
            {
                Console.WriteLine($"error al borrar reseña: {ex.Message}");
                return false;
            }
        }
    }
}
//asincronia = todos los metodos son asincronos, lo que es importante para operaciones 
//E/S como acceso a base de datos
