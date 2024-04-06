using System;
using System.Collections.Generic;

namespace CampusVirtual.Models
{
    public partial class Materia
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? Anio { get; set; }
        public int? Cuatrimestre { get; set; }
        public int? HorasSemanales { get; set; }
        public int IdCarrera { get; set; }

        public virtual Carrera IdCarreraNavigation { get; set; } = null!;
    }
}
