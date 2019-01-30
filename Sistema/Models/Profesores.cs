using System;
using System.Collections.Generic;

namespace Sistema.Models
{
    public partial class Profesores
    {
        public Profesores()
        {
            Alumnos = new HashSet<Alumnos>();
        }

        public int ProfesorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Edad { get; set; }
        public string Correo { get; set; }
        public int Materia { get; set; }
        public string Pass { get; set; }

        public Materia MateriaNavigation { get; set; }
        public ICollection<Alumnos> Alumnos { get; set; }
    }
}
