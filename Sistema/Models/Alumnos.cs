using System;
using System.Collections.Generic;

namespace Sistema.Models
{
    public partial class Alumnos
    {
        public int AlumnoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Edad { get; set; }
        public int Materia { get; set; }
        public int Profesor { get; set; }
        public string Pass { get; set; }

        public Materia MateriaNavigation { get; set; }
        public Profesores ProfesorNavigation { get; set; }
    }
}
