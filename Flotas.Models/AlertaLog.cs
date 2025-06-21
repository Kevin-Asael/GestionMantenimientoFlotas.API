using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Flotas.Models
{
    public class AlertaLog
    {
        [Key] public int Id { get; set; }

        public int CamionId { get; set; }  // Relación con el Camion

        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool EstaResuelta { get; set; }

        [JsonIgnore]
        public Camion? Camion { get; set; }

    }
}
