using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flotas.Models
{
    public class Admin
    {
        [Key] public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Recuerda encriptar la contraseña en producción
    }
}
