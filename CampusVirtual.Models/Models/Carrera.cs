using System;
using System.Collections.Generic;

namespace CampusVirtual.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Materia = new HashSet<Materia>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? CantAnios { get; set; }

        public virtual ICollection<Materia> Materia { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
