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
    public class Camion
    {
        [Key]
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public double KilometrajeActual { get; set; }
        public string Estado { get; set; }

        public int ConductorId { get; set; }

        [JsonIgnore]
        public Conductor? Conductor { get; set; }

        // Relación uno a muchos con MantenimientoProgramado
        [JsonIgnore]
        public ICollection<MantenimientoProgramado>? Mantenimientos { get; set; }
        // Relación uno a muchos con Alertas
        [JsonIgnore] public ICollection<AlertaLog>? Alertas { get; set; }  // Relación de alertas con el camión

        // Relación uno a muchos con SensorLogs
        [JsonIgnore] public ICollection<SensorLog>? SensorLogs { get; set; }  // Relación de logs de sensores con el camión
    }
}
