using Flotas.Models;
using GestionFlota.API.Consumer;
using Microsoft.AspNetCore.Mvc;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class SensoresLogsController : Controller
    {
        // GET: SensorLogs
        public ActionResult Index()
        {
            var logs = Crud<SensorLog>.GetAll();  // Obtiene todos los registros de SensorLog desde la API de Logs
            return View(logs);  // Muestra la lista de registros de SensorLog en la vista
        }

        // GET: SensorLogs/Details/5
        public ActionResult Details(int id)
        {
            var log = Crud<SensorLog>.GetById(id);  // Obtiene un SensorLog por ID desde la API de Logs
            if (log == null) return NotFound();  // Si no se encuentra el log, retorna NotFound()

            // Obtener el camión asociado al SensorLog desde la API de Gestión
            log.Camion = Crud<Camion>.GetById(log.CamionId);  // Aquí obtenemos el camión relacionado con el SensorLog

            // Si no se encuentra el camión, mostrar un error
            if (log.Camion == null)
            {
                return NotFound();  // Si no se encuentra el camión, retornamos un error
            }

            return View(log);  // Muestra los detalles del SensorLog junto con el camión asociado
        }

    }
}