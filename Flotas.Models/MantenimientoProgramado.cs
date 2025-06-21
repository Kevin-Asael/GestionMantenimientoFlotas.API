using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Flotas.Models
{
    public class MantenimientoProgramado
    {
        [Key] public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMantenimiento { get; set; }

        // FK's
        public int CamionId { get; set; }  // Relación uno a muchos con Camion
        public int TallerId { get; set; }  // Relación uno a muchos con Taller

        // Navigation properties
        [JsonIgnore] public Camion? Camion { get; set; }
        [JsonIgnore] public Taller? Taller { get; set; }
    }
}
