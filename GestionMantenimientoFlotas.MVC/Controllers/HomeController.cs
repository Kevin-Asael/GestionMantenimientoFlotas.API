using Flotas.Models;
using GestionFlota.API.Consumer;
using GestionMantenimientoFlotas.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GestionMantenimientoFlotas.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home/Index
        public ActionResult Index(int page = 1, int pageSize = 10, string searchQuery = "", int? camionId = null, int? tallerId = null)
        {
            // Si no hay usuario en sesión, forzar login
            if (HttpContext.Session.GetString("User") == null)
                return RedirectToAction("Login", "Auth");

            var mantenimientos = Crud<MantenimientoProgramado>.GetAll();

            if (!string.IsNullOrEmpty(searchQuery))
                mantenimientos = mantenimientos.Where(m => m.TipoMantenimiento.Contains(searchQuery)).ToList();

            if (camionId.HasValue)
                mantenimientos = mantenimientos.Where(m => m.CamionId == camionId.Value).ToList();

            if (tallerId.HasValue)
                mantenimientos = mantenimientos.Where(m => m.TallerId == tallerId.Value).ToList();

            var totalItems = mantenimientos.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var data = mantenimientos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.CamionId = camionId;
            ViewBag.TallerId = tallerId;
            ViewBag.Camiones = getCamiones();
            ViewBag.Talleres = getTalleres();

            return View(data);
        }

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        private List<SelectListItem> getCamiones()
        {
            return Crud<Camion>.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Placa
                }).ToList();
        }

        private List<SelectListItem> getTalleres()
        {
            return Crud<Taller>.GetAll()
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nombre
                }).ToList();
        }
    }
}