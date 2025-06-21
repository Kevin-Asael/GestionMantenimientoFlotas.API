using Flotas.Models;
using GestionFlota.API.Consumer;
using GestionMantenimientoFlotas.MVC.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class Camiones : Controller
    {
        // GET: Camiones
        public ActionResult Index()
        {
            var data = Crud<Camion>.GetAll();  // Obtiene todos los camiones
            return View(data);
        }

        // GET: Camiones/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Camion>.GetById(id);  
            if (data == null) return NotFound();

            data.Conductor = Crud<Conductor>.GetById(data.ConductorId);


            data.Mantenimientos = Crud<MantenimientoProgramado>.GetBy("Camion", id).ToList();

            
            data.Alertas = Crud<AlertaLog>.GetBy("Camion", id);  

           
            data.SensorLogs = Crud<SensorLog>.GetBy("Camion", id);  

            return View(data);  
        }

        // Generar el PDF y devolverlo como un archivo descargable
        public IActionResult DescargarPdf(int id)
        {
            // Obtener el camión
            var camion = Crud<Camion>.GetById(id);
            if (camion == null)
            {
                return NotFound();
            }

            // Generar el PDF
            var pdfContent = ServiciosGenerarPDF.GenerarPdfDeLogsYAlertas(camion);

            // Devolver el PDF como archivo descargable
            return File(pdfContent, "application/pdf", $"Reporte_Camion_{camion.Placa}.pdf");
        }

        // POST: Camiones/GenerarAlertasYLogs/5
        [HttpPost]
        public ActionResult GenerarAlertasYLogs(int id)
        {
            // Obtiene el camión desde la API de Gestión
            var camion = Crud<Camion>.GetById(id);
            if (camion == null)
            {
                return NotFound();
            }

            // Llamamos al servicio para generar alertas y logs
            ServiciosAlertas.GenerarLogYAlertas(camion);

            // Después de generar, redirigimos al usuario a la vista de detalles del camión
            return RedirectToAction("Details", new { id = id });
        }



        // GET: Camiones/Create
        public ActionResult Create()
        {
            ViewBag.Conductores = getConductores();  // Obtiene la lista de conductores para el dropdown
            return View();
        }

        private List<SelectListItem> getConductores()
        {
            var conductores = Crud<Conductor>.GetAll();  // Obtiene todos los conductores
            return conductores.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre  
            }).ToList();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Camion data)
        {
            try
            {
                // Asegúrate de que el ConductorId esté correctamente asignado
                if (data.ConductorId == 0)
                {
                    ModelState.AddModelError("", "Debe seleccionar un conductor.");
                    return View(data);
                }

                // Intenta crear un nuevo camión
                Crud<Camion>.Create(data);  // Crea el nuevo camión en la base de datos

                // Llamamos al servicio para generar logs y alertas
                ServiciosAlertas.GenerarLogYAlertas(data);  // Lógica para crear los logs y alertas

                // Redirige a la vista Index
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si ocurre un error, muestra un mensaje de error
                ModelState.AddModelError("", "An error occurred while creating the Camion. Please try again.");
                return View(data);  // Vuelve a la vista de creación si hay un error
            }
        }

        // GET: Camiones/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Conductores = getConductores();  // Obtiene la lista de conductores para el dropdown
            var data = Crud<Camion>.GetById(id);  // Obtiene el camión por ID para editarlo
            return View(data);
        }

        // POST: Camiones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Camion data)
        {
            try
            {
                // Asegúrate de que el ConductorId esté correctamente asignado
                if (data.ConductorId == 0)
                {
                    ModelState.AddModelError("", "Debe seleccionar un conductor.");
                    return View(data);
                }

                // Intenta actualizar el camión
                Crud<Camion>.Update(id, data);  // Actualiza el camión en la base de datos

                // Llamamos al servicio para generar logs y alertas
                ServiciosAlertas.GenerarLogYAlertas(data);  // Generación de log y alerta

                // Redirige a la vista Index
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the Camion. Please try again.");
                return View(data);  // Vuelve a la vista de edición si hay un error
            }
        }

        // GET: Camiones/Delete/5
        public ActionResult Delete(int id)
        {

            var data = Crud<Camion>.GetById(id);  // Obtiene el camión por ID para confirmación de eliminación
            data.Conductor = Crud<Conductor>.GetById(data.ConductorId);  // Obtiene el conductor asociado al camión
            return View(data);
        }

        // POST: Camiones/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Camion data)
        {
            try
            {
                // Intenta eliminar el camión
                Crud<Camion>.Delete(id);
                return RedirectToAction(nameof(Index));  // Redirige a la vista Index
            }
            catch (Exception ex)
            {
                // Maneja la excepción si algo sale mal
                ModelState.AddModelError("", "An error occurred while deleting the Camion. Please try again.");
                return View(data);  // Vuelve a la vista de eliminación si hay un error
            }
        }
    }
}