namespace CampusVirtual.Models.Dto
{
    public class UserDto
    {
        public int Id{ get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Carrera { get; set; }
        public string? Rol { get; set; }

        public UserDto()
        {
            
        }

        public UserDto(int id, string nombre, string apellido, string dni, string correo, string carrera, string rol) : base()
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Dni = dni;
            this.Correo = correo;
            this.Carrera = carrera;
            this.Rol = rol;
        }
    }
}
