using Flotas.Models;
using GestionFlota.API.Consumer;
using Microsoft.AspNetCore.Mvc;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class AlertasLogsController : Controller
    {
        // GET: AlertasLogs
        public ActionResult Index()
        {
            var alertas = Crud<AlertaLog>.GetAll();  // Obtiene todas las alertas desde la API de Logs
            return View(alertas);  // Muestra la lista de alertas en la vista
        }

        // GET: AlertasLogs/Details/5
        public ActionResult Details(int id)
        {
            var alerta = Crud<AlertaLog>.GetById(id);  // Obtiene una AlertaLog por ID desde la API de Logs
            if (alerta == null) return NotFound();

            // Obtener el camión asociado a la AlertaLog desde la API de Gestión
            alerta.Camion = Crud<Camion>.GetById(alerta.CamionId);

            return View(alerta);  // Muestra los detalles de la AlertaLog junto con el camión asociado
        }
    }
}
