using System;
using Flotas.Models;
using GestionFlota.API.Consumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GestionMantenimientoFlotas.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1) Configurar los endpoints de la API para cada uno de los modelos
            Crud<Admin>.EndPoint = "https://localhost:7142/api/Admins";
            Crud<Camion>.EndPoint = "https://localhost:7142/api/Camiones";
            Crud<Conductor>.EndPoint = "https://localhost:7142/api/Conductores";
            Crud<MantenimientoProgramado>.EndPoint = "https://localhost:7142/api/MantenimientosProgramados";
            Crud<Taller>.EndPoint = "https://localhost:7142/api/Talleres";
            Crud<AlertaLog>.EndPoint = "https://localhost:7256/api/AlertasLogs";
            Crud<SensorLog>.EndPoint = "https://localhost:7256/api/SensoresLogs";

            // 2) Crear y configurar el builder de la aplicación
            var builder = WebApplication.CreateBuilder(args);

            // 3) Añadir MVC
            builder.Services.AddControllersWithViews();

            // 4) Registrar IHttpContextAccessor para poder leer HttpContext.Session en las vistas
            builder.Services.AddHttpContextAccessor();

            // 5) Configurar sesiones
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            var app = builder.Build();

            // 6) Pipeline de solicitudes HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 7) Habilitar sesión antes de Authorization
            app.UseSession();

            app.UseAuthorization();

            // 8) Ruta por defecto: arrancar en Auth/Login
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            // 9) Ejecutar la aplicación
            app.Run();
        }
    }
}