﻿using System;
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
    public class CamionesController : ControllerBase
    {
        private readonly GestionMantenimientoFlotasAPIContext _context;

        public CamionesController(GestionMantenimientoFlotasAPIContext context)
        {
            _context = context;
        }

        // GET: api/Camiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camion>>> GetCamion()
        {
            var data = await _context.Camiones
                .Include(c => c.Conductor)
                .ToListAsync();
            return data;
        }


        [HttpGet("Conductor/{id}")]
        public async Task<ActionResult<IEnumerable<Camion>>> GetCamionesByConductor(int id)
        {
            var data = await _context.Camiones
                .Where(c => c.ConductorId == id)
                .ToListAsync();
            return data;
        }

        // GET: api/Camiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camion>> GetCamion(int id)
        {
            var camion = await _context.Camiones.FindAsync(id);

            if (camion == null)
            {
                return NotFound();
            }

            return camion;
        }

        // PUT: api/Camiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamion(int id, Camion camion)
        {
            if (id != camion.Id)
            {
                return BadRequest();
            }

            _context.Entry(camion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamionExists(id))
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

        // POST: api/Camiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Camion>> PostCamion(Camion camion)
        {
            _context.Camiones.Add(camion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamion", new { id = camion.Id }, camion);
        }

        // DELETE: api/Camiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamion(int id)
        {
            var camion = await _context.Camiones.FindAsync(id);
            if (camion == null)
            {
                return NotFound();
            }

            _context.Camiones.Remove(camion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CamionExists(int id)
        {
            return _context.Camiones.Any(e => e.Id == id);
        }
    }
}
