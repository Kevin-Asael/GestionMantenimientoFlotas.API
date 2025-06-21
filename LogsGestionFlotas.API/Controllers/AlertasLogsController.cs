using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flotas.Models;

namespace LogsGestionFlotas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertasLogsController : ControllerBase
    {
        private readonly LogsGestionFlotasAPIContext _context;

        public AlertasLogsController(LogsGestionFlotasAPIContext context)
        {
            _context = context;
        }

        // GET: api/AlertasLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaLog>>> GetAlertaLog()
        {
            return await _context.AlertaLog.ToListAsync();
        }

        // GET: api/AlertasLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaLog>> GetAlertaLog(int id)
        {
            var alertaLog = await _context.AlertaLog.FindAsync(id);

            if (alertaLog == null)
            {
                return NotFound();
            }

            return alertaLog;
        }

        // PUT: api/AlertasLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaLog>> PostAlertaLog(AlertaLog alertaLog)
        {
            _context.AlertaLog.Add(alertaLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaLog", new { id = alertaLog.Id }, alertaLog);
        }

        // DELETE: api/AlertasLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaLog(int id)
        {
            var alertaLog = await _context.AlertaLog.FindAsync(id);
            if (alertaLog == null)
            {
                return NotFound();
            }

            _context.AlertaLog.Remove(alertaLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertaLogExists(int id)
        {
            return _context.AlertaLog.Any(e => e.Id == id);
        }
    }
}
