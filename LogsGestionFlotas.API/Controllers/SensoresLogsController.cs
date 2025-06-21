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
    public class SensoresLogsController : ControllerBase
    {
        private readonly LogsGestionFlotasAPIContext _context;

        public SensoresLogsController(LogsGestionFlotasAPIContext context)
        {
            _context = context;
        }

        // GET: api/SensoresLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorLog>>> GetSensorLog()
        {
            return await _context.SensorLog.ToListAsync();
        }

        // GET: api/SensoresLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorLog>> GetSensorLog(int id)
        {
            var sensorLog = await _context.SensorLog.FindAsync(id);

            if (sensorLog == null)
            {
                return NotFound();
            }

            return sensorLog;
        }

        // PUT: api/SensoresLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SensorLog>> PostSensorLog(SensorLog sensorLog)
        {
            _context.SensorLog.Add(sensorLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensorLog", new { id = sensorLog.Id }, sensorLog);
        }

        // DELETE: api/SensoresLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorLog(int id)
        {
            var sensorLog = await _context.SensorLog.FindAsync(id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            _context.SensorLog.Remove(sensorLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorLogExists(int id)
        {
            return _context.SensorLog.Any(e => e.Id == id);
        }
    }
}
