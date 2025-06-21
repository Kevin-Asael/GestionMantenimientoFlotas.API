using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Flotas.Models
{
    public class Taller
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public int CapacidadMaximaReparacionesSimultaneas { get; set; }

        // Relación uno a muchos: Un Taller puede tener muchos MantenimientosProgramados
        [JsonIgnore] public ICollection<MantenimientoProgramado>? MantenimientosProgramados { get; set; }
    }
}
