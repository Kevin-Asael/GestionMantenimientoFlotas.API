using Flotas.Models;
using GestionFlota.API.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class Talleres : Controller
    {
        // GET: Talleres
        public ActionResult Index()
        {
            var data = Crud<Taller>.GetAll();
            return View(data);
        }

        // GET: Talleres/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Taller>.GetById(id);
            data.MantenimientosProgramados = Crud<MantenimientoProgramado>.GetBy("Taller", id);
            return View(data);
        }

        // GET: Talleres/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Talleres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Taller data)
        {
            try
            {
                Crud<Taller>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // Log the exception (ex) if necessary
                ModelState.AddModelError("", "An error occurred while creating the Taller. Please try again.");
                return View(data);
            }
        }

        // GET: Talleres/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Taller>.GetById(id);
            return View(data);
        }

        // POST: Talleres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Taller data)
        {
            try
            {
                Crud<Taller>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // Log the exception (ex) if necessary
                ModelState.AddModelError("", "An error occurred while updating the Taller. Please try again.");
                return View(data);
            }
        }

        // GET: Talleres/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Taller>.GetById(id);
            return View(data);
        }

        // POST: Talleres/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Taller data)
        {
            try
            {
                Crud<Taller>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                // Log the exception (ex) if necessary
                ModelState.AddModelError("", "An error occurred while deleting the Taller. Please try again.");
                return View(data);
            }
        }
    }
}
