using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flotas.Models;

namespace GestionMantenimientoFlotas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosProgramadosController : ControllerBase
    {
        private readonly GestionMantenimientoFlotasAPIContext _context;

        public MantenimientosProgramadosController(GestionMantenimientoFlotasAPIContext context)
        {
            _context = context;
        }

        // GET: api/MantenimientosProgramados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MantenimientoProgramado>>> GetMantenimientoProgramado()
        {
            var data = await _context.MantenimientosProgramados
                .Include(mp => mp.Camion)
                .Include(mp => mp.Taller)
                .ToListAsync();
            return data;
        }

        [HttpGet("Taller/{id}")]
        public async Task<ActionResult<IEnumerable<MantenimientoProgramado>>> GetMantenimientosByTaller(int id)
        {
            var data = await _context.MantenimientosProgramados
                .Where(mp => mp.TallerId == id)
                .Include(mp => mp.Camion)
                .ToListAsync();
            return data;
        }

        [HttpGet("Camion/{id}")]
        public async Task<ActionResult<IEnumerable<MantenimientoProgramado>>> GetMantenimientosByCamion(int id)
        {
            var data = await _context.MantenimientosProgramados
                .Where(mp => mp.CamionId == id)
                .Include(mp => mp.Taller)
                .ToListAsync();
            return data;
        }

        // GET: api/MantenimientosProgramados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MantenimientoProgramado>> GetMantenimientoProgramado(int id)
        {
            var mantenimientoProgramado = await _context.MantenimientosProgramados.FindAsync(id);

            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }

            return mantenimientoProgramado;
        }

        // PUT: api/MantenimientosProgramados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMantenimientoProgramado(int id, MantenimientoProgramado mantenimientoProgramado)
        {
            if (id != mantenimientoProgramado.Id)
            {
                return BadRequest();
            }

            _context.Entry(mantenimientoProgramado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoProgramadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MantenimientosProgramados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MantenimientoProgramado>> PostMantenimientoProgramado(MantenimientoProgramado mantenimientoProgramado)
        {
            _context.MantenimientosProgramados.Add(mantenimientoProgramado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMantenimientoProgramado", new { id = mantenimientoProgramado.Id }, mantenimientoProgramado);
        }

        // DELETE: api/MantenimientosProgramados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMantenimientoProgramado(int id)
        {
            var mantenimientoProgramado = await _context.MantenimientosProgramados.FindAsync(id);
            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }

            _context.MantenimientosProgramados.Remove(mantenimientoProgramado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MantenimientoProgramadoExists(int id)
        {
            return _context.MantenimientosProgramados.Any(e => e.Id == id);
        }
    }
}
