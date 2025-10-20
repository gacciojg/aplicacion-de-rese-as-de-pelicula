using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data.Common;
using Microsoft.Maui.Storage;
using AplicacionPelicula.Servicios;
using AplicacionPelicula.BaseDatos;

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

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("AppDb");
            });//appdbcontext registrado usando una base de datos en memoria

            builder.Services.AddHttpClient<ReseniaService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
            });//registo httpclient

            //registrar servicios
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddScoped<PeliculaService>();
            //builder.Services.AddScoped<ReseniaService>(); 
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<LoginService>();
            return builder.Build();
        }
    }
}
