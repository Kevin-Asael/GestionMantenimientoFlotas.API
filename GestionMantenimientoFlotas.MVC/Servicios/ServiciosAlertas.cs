using Flotas.Models;
using GestionFlota.API.Consumer;
using System.Linq;

namespace GestionMantenimientoFlotas.MVC.Servicios
{
    public static class ServiciosAlertas
    {
        public static void GenerarLogYAlertas(Camion camion)
        {
            Random random = new Random();
            int incrementoKilometraje = random.Next(1, 1001);  // Genera un número aleatorio entre 1 y 1000

            camion.KilometrajeActual += incrementoKilometraje;  // Aumenta el kilometraje en un valor aleatorio

            Crud<Camion>.Update(camion.Id, camion);  // Actualiza el camión con el nuevo kilometraje

            // Crear el log de sensor
            Crud<SensorLog>.Create(new SensorLog
            {
                CamionId = camion.Id,
                Timestamp = DateTime.UtcNow,
                Kilometraje = camion.KilometrajeActual,  // Usamos el kilometraje actualizado
                EstadoMotor = camion.Estado
            });

            if (camion.KilometrajeActual >= 5000)
            {
                // Crear la alerta de "Motor Desgastado"
                Crud<AlertaLog>.Create(new AlertaLog
                {
                    CamionId = camion.Id,
                    Descripcion = "Motor Desgastado - Kilometraje superior a 5000 km",
                    Fecha = DateTime.UtcNow,
                    EstaResuelta = false  // Estado inicial: no resuelta
                });

                Crud<SensorLog>.Create(new SensorLog
                {
                    CamionId = camion.Id,
                    Timestamp = DateTime.UtcNow,
                    Kilometraje = camion.KilometrajeActual,
                    EstadoMotor = "Motor Desgastado - Necesita mantenimiento"
                });
            }

            var mantenimientos = Crud<MantenimientoProgramado>.GetBy("Camion", camion.Id).ToList();
            var alertas = Crud<AlertaLog>.GetAll();  // Obtén todas las alertas existentes

            // === No asignado mantenimiento o fuera de plazo ===
            var alertaNoMantenimiento = alertas.FirstOrDefault(a =>
                a.CamionId == camion.Id && a.Descripcion == "No asignado mantenimiento");

            if (!mantenimientos.Any())
            {
                if (alertaNoMantenimiento == null)
                {
                    // Crear una nueva alerta para "sin mantenimiento asignado"
                    Crud<AlertaLog>.Create(new AlertaLog
                    {
                        CamionId = camion.Id,
                        Descripcion = "El camión no tiene mantenimiento programado.",
                        Fecha = DateTime.UtcNow,
                        EstaResuelta = false  // Estado inicial: no resuelta
                    });

                    // Crear un log de sensor para indicar el riesgo sin mantenimiento
                    Crud<SensorLog>.Create(new SensorLog
                    {
                        CamionId = camion.Id,
                        Timestamp = DateTime.UtcNow,
                        Kilometraje = camion.KilometrajeActual,
                        EstadoMotor = "Riesgo de fallo sin mantenimiento"
                    });
                }
            }
            else
            {
                var mantenimientoReciente = mantenimientos.OrderByDescending(m => m.Fecha).First();

                if (alertaNoMantenimiento != null)
                {
                    if (mantenimientoReciente.Fecha <= DateTime.UtcNow.AddDays(30))
                    {
                        // Si el mantenimiento está reciente, cerramos la alerta
                        alertaNoMantenimiento.EstaResuelta = true;
                        Crud<AlertaLog>.Update(alertaNoMantenimiento.Id, alertaNoMantenimiento);
                    }
                    else
                    {
                        // Si el mantenimiento está fuera de plazo, dejamos la alerta abierta
                        alertaNoMantenimiento.EstaResuelta = false;
                        Crud<AlertaLog>.Update(alertaNoMantenimiento.Id, alertaNoMantenimiento);
                    }
                }
            }
        }
    }
}
