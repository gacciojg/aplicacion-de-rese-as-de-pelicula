using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionPelicula.BaseDatos;
using AplicacionPelicula.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AplicacionPelicula.Servicios
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;//por medio de una dependencia inyectada asegura acceso centralizado a las entidades

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodosUsuariosAsync()
        {
            return await _context.Usuarios
                .OrderBy(u => u.Username)//ordena usuarios alfabeticamenten
                .ToListAsync();//ejecuta  eso de manera asincrona
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);//inserta el usuario en el dbset 
            await _context.SaveChangesAsync();//persisten los cambios de manera asincrona
        }

        public async Task<bool> UsuarioExisteAsync(string username)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Username == username);//true o false si existe un usuario con el mismo nombre
        }
    }
}
