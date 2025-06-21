using Flotas.Models;
using GestionFlota.API.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class Conductores : Controller
    {
        // GET: Conductores
        public ActionResult Index()
        {
            var data = Crud<Conductor>.GetAll();  
            return View(data);  
        }

        // GET: Conductores/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Conductor>.GetById(id);  
            data.Camiones = Crud<Camion>.GetBy("Conductor", id);  
            return View(data);  
        }

        // GET: Conductores/Create
        public ActionResult Create()
        {
            return View(); 
        }

        // POST: Conductores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Conductor data)
        {
            try
            {
                Crud<Conductor>.Create(data);  
                return RedirectToAction(nameof(Index));  
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An error occurred while creating the Conductor. Please try again.");
                return View(data); 
            }
        }

        // GET: Conductores/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Conductor>.GetById(id);  
            return View(data);  
        }

        // POST: Conductores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Conductor data)
        {
            try
            {
                Crud<Conductor>.Update(id, data);  
                return RedirectToAction(nameof(Index));  
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An error occurred while updating the Conductor. Please try again.");
                return View(data);  
            }
        }

        // GET: Conductores/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Conductor>.GetById(id);  
            return View(data);  
        }

        // POST: Conductores/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Conductor data)
        {
            try
            {
                Crud<Conductor>.Delete(id);  
                return RedirectToAction(nameof(Index));  
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the Conductor. Please try again.");
                return View(data);  
            }
        }
    }
}
