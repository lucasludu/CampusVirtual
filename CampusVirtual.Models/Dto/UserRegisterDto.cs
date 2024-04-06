namespace CampusVirtual.Models.Dto
{
    public class UserRegisterDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? IdCarrera { get; set; }
        public int? IdRol { get; set; }


        public UserRegisterDto()
        {
            this.IdCarrera = (IdRol == 1) ? 0 : this.IdCarrera;
        }


        public UserRegisterDto(string nombre, string apellido, string dni, string correo, string password, int idrol) : base()
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Dni = dni;
            this.Correo = correo;
            this.Password = password;
            this.IdCarrera = (idrol == 1) ? 0 : this.IdCarrera;
            this.IdRol = idrol;
        }

    }
}
