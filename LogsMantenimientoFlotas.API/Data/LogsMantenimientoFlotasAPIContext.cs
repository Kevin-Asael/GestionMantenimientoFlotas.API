using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flotas.Models;

    public class LogsMantenimientoFlotasAPIContext : DbContext
    {
        public LogsMantenimientoFlotasAPIContext (DbContextOptions<LogsMantenimientoFlotasAPIContext> options)
            : base(options)
        {
        }

public DbSet<Flotas.Models.AlertaLog> AlertasLogs { get; set; } = default!;

public DbSet<Flotas.Models.SensorLog> SensoresLogs { get; set; } = default!;

        
    }
