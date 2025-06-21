using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Flotas.Models
{
    public class SensorLog
    {
        [Key] public int Id { get; set; }

        public int CamionId { get; set; }  // Relación con el Camion

        public double Kilometraje { get; set; }
        public string EstadoMotor { get; set; }

        // Timestamp de la lectura
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public Camion? Camion { get; set; }
    }
}
