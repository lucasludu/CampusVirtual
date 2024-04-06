namespace CampusVirtual.Models.Dto
{
    public class CarreraInsertDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int CantAnios { get; set; }


        public CarreraInsertDto()
        {
            
        }


        public CarreraInsertDto(string nombre, string descripcion, int cantAnios) : base()
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.CantAnios = cantAnios;
        }

    }
}
