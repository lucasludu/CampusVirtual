using System;
using System.Collections.Generic;

namespace CampusVirtual.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public int? IdCarrera { get; set; }
        public int IdRol { get; set; }

        public virtual Carrera? IdCarreraNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}
