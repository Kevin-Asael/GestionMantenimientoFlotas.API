using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Flotas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlotasLogs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertasLogsController : ControllerBase
    {
        private readonly LogsMantenimientoFlotasAPIContext _context;

        public AlertasLogsController(LogsMantenimientoFlotasAPIContext context)
        {
            _context = context;
        }

        // GET: api/AlertasLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaLog>>> GetAlertaLog()
        {
            var data = await _context.AlertasLogs.ToListAsync();
            return data;
        }



        // GET: api/AlertasLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaLog>> GetAlertaLog(int id)
        {
            var alertaLog = await _context.AlertasLogs.FindAsync(id);

            if (alertaLog == null)
            {
                return NotFound();
            }

            return alertaLog;
        }

        [HttpGet("Camion/{id}")]
        public async Task<ActionResult<IEnumerable<AlertaLog>>> GetAlertasByCamion(int id)
        {
            // Obtener todas las alertas relacionadas con el camión por ID
            var alertas = await _context.AlertasLogs
                .Where(a => a.CamionId == id)  // Filtramos las alertas por el ID del camión
                .ToListAsync();

            if (alertas == null || !alertas.Any())
            {
                return NotFound();  // Si no hay alertas, retornar "NotFound"
            }

            return alertas;  // Retornar las alertas asociadas al camión
        }

        // PUT: api/AlertasLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaLog(int id, AlertaLog alertaLog)
        {
            if (id != alertaLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(alertaLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaLogExists(id))
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

        // POST: api/AlertasLogs
        [HttpPost]
        public async Task<ActionResult<AlertaLog>> PostAlertaLog(AlertaLog alertaLog)
        {
            _context.AlertasLogs.Add(alertaLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlertaLog), new { id = alertaLog.Id }, alertaLog);
        }

        // DELETE: api/AlertasLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaLog(int id)
        {
            var alertaLog = await _context.AlertasLogs.FindAsync(id);
            if (alertaLog == null)
            {
                return NotFound();
            }

            _context.AlertasLogs.Remove(alertaLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool AlertaLogExists(int id)
        {
            return _context.AlertasLogs.Any(e => e.Id == id);
        }
    }
}
