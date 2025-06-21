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
    public class Conductor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Licencia { get; set; }
        public DateTime FechaVencimiento { get; set; }

        [JsonIgnore]
        public ICollection<Camion>? Camiones { get; set; } 
    }

}
