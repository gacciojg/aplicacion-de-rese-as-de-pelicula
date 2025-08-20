using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data.Common;
using Microsoft.Maui.Storage;
using AplicacionPelicula.Modelos;

namespace AplicacionPelicula
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder(); //configura todos los componentes de la app
            builder
                .UseMauiApp<App>() //registra la clase principal app
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            //registrar servicios
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddScoped<PeliculaService>();
            builder.Services.AddScoped<ReseniaService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<LoginService>();

            var app = builder.Build();

            // Inicialización INFALIBLE de la base de datos
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Borra y recrea la DB (solo en desarrollo)
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // Verificación de tablas (debug)
                var tables = db.Database.SqlQueryRaw<string>(
                    "SELECT name FROM sqlite_master WHERE type='table';").ToList();
                System.Diagnostics.Debug.WriteLine($"Tablas creadas: {string.Join(", ", tables)}");
            }
            return app;
        }
    }
}
