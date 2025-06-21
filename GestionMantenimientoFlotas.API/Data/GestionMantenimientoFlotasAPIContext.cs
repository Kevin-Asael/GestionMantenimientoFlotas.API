using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flotas.Models;

    public class GestionMantenimientoFlotasAPIContext : DbContext
    {
        public GestionMantenimientoFlotasAPIContext (DbContextOptions<GestionMantenimientoFlotasAPIContext> options)
            : base(options)
        {
        }

public DbSet<Flotas.Models.Admin> Admins { get; set; } = default!;

public DbSet<Flotas.Models.Camion> Camiones { get; set; } = default!;

public DbSet<Flotas.Models.Conductor> Conductores { get; set; } = default!;

public DbSet<Flotas.Models.MantenimientoProgramado> MantenimientosProgramados { get; set; } = default!;

public DbSet<Flotas.Models.Taller> Talleres { get; set; } = default!;





    }
