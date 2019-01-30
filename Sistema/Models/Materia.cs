using System;
using System.Collections.Generic;

namespace Sistema.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Alumnos = new HashSet<Alumnos>();
            Profesores = new HashSet<Profesores>();
        }

        public int MateriaId { get; set; }
        public string NombreMateria { get; set; }

        public ICollection<Alumnos> Alumnos { get; set; }
        public ICollection<Profesores> Profesores { get; set; }
    }
}
