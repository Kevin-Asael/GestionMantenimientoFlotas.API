using Flotas.Models;
using GestionFlota.API.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class MantenimientosProgramadosController : Controller
    {
        // GET: MantenimientosProgramadosController
        public ActionResult Index()
        {
            var data = Crud<MantenimientoProgramado>.GetAll();
            return View(data);
        }

        // GET: MantenimientosProgramadosController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<MantenimientoProgramado>.GetById(id);
            data.Camion = Crud<Camion>.GetById(data.CamionId);
            data.Taller = Crud<Taller>.GetById(data.TallerId);
            return View(data);
        }

        private List<SelectListItem> getCamion()
        {
            var camiones = Crud<Camion>.GetAll();
            return camiones.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Placa
            }).ToList();
        }

        private List<SelectListItem> getTaller()
        {
            var talleres = Crud<Taller>.GetAll();
            return talleres.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre
            }).ToList();
        }

        

        // GET: MantenimientosProgramadosController/Create
        public ActionResult Create()
        {
            ViewBag.Camiones = getCamion();
            ViewBag.Talleres = getTaller();
            return View();
        }

        // POST: MantenimientosProgramadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoProgramado data)
        {
            try
            {
                Crud<MantenimientoProgramado>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear el mantenimiento programado: {ex.Message}");
                return View(data);
            }
        }

        // GET: MantenimientosProgramadosController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Camiones = getCamion();
            ViewBag.Talleres = getTaller();
            var data = Crud<MantenimientoProgramado>.GetById(id);
            return View(data);
        }

        // POST: MantenimientosProgramadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MantenimientoProgramado data)
        {
            try
            {
                Crud<MantenimientoProgramado>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"Error al editar el mantenimiento programado: {ex.Message}");
                return View(data);
            }
        }

        // GET: MantenimientosProgramadosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<MantenimientoProgramado>.GetById(id);
            data.Camion = Crud<Camion>.GetById(data.CamionId);
            data.Taller = Crud<Taller>.GetById(data.TallerId);
            return View(data);
        }

        // POST: MantenimientosProgramadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MantenimientoProgramado data)
        {
            try
            {
                Crud<MantenimientoProgramado>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar el mantenimiento programado: {ex.Message}");
                return View(data);
            }
        }
    }
}
