using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Login
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}
