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
    public class SensoresLogsController : ControllerBase
    {
        private readonly LogsMantenimientoFlotasAPIContext _context;

        public SensoresLogsController(LogsMantenimientoFlotasAPIContext context)
        {
            _context = context;
        }

        // GET: api/SensoresLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorLog>>> GetSensorLog()
        {
            var data = await _context.SensoresLogs.ToListAsync();
            return data;
        }

        [HttpGet("Camion/{id}")]
        public async Task<ActionResult<IEnumerable<SensorLog>>> GetSensorLogsByCamion(int id)
        {
            // Obtener todos los logs de sensor relacionados con el camión por ID
            var sensorLogs = await _context.SensoresLogs
                .Where(sl => sl.CamionId == id)  // Filtramos los logs por el ID del camión
                .ToListAsync();

            if (sensorLogs == null || !sensorLogs.Any())
            {
                return NotFound();  // Si no hay logs, retornar "NotFound"
            }

            return sensorLogs;  // Retornar los logs de sensores asociados al camión
        }

        // GET: api/SensoresLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorLog>> GetSensorLog(int id)
        {
            var sensorLog = await _context.SensoresLogs.FindAsync(id);

            if (sensorLog == null)
            {
                return NotFound();
            }

            return sensorLog;
        }

        // PUT: api/SensoresLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorLog(int id, SensorLog sensorLog)
        {
            if (id != sensorLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensorLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorLogExists(id))
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

        // POST: api/SensoresLogs
        [HttpPost]
        public async Task<ActionResult<SensorLog>> PostSensorLog(SensorLog sensorLog)
        {
            _context.SensoresLogs.Add(sensorLog);
            await _context.SaveChangesAsync();

            // Verificar lógica para alertas (si es necesario)
            return CreatedAtAction("GetSensorLog", new { id = sensorLog.Id }, sensorLog);
        }

        // DELETE: api/SensoresLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorLog(int id)
        {
            var sensorLog = await _context.SensoresLogs.FindAsync(id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            _context.SensoresLogs.Remove(sensorLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorLogExists(int id)
        {
            return _context.SensoresLogs.Any(e => e.Id == id);
        }
    }
}
