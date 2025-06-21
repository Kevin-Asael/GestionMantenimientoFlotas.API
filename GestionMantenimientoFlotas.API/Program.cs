using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestionMantenimientoFlotas.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GestionMantenimientoFlotasAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GestionMantenimientoFlotasAPIContext") ?? throw new InvalidOperationException("Connection string 'GestionMantenimientoFlotasAPIContext' not found.")));

            // Add services to the container.
            builder.Services
            .AddControllers()
            .AddNewtonsoftJson(                                                   //Nuevo
                options => options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
