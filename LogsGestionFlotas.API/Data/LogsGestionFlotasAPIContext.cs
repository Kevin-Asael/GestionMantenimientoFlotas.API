using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flotas.Models;

    public class LogsGestionFlotasAPIContext : DbContext
    {
        public LogsGestionFlotasAPIContext (DbContextOptions<LogsGestionFlotasAPIContext> options)
            : base(options)
        {
        }

public DbSet<Flotas.Models.SensorLog> SensorLog { get; set; } = default!;

public DbSet<Flotas.Models.AlertaLog> AlertaLog { get; set; } = default!;

    }
