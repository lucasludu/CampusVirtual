namespace CampusVirtual.Models.Dto
{
    public class CarreraUpdateDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? CantAnios { get; set; }


        public CarreraUpdateDto()
        {
            
        }


        public CarreraUpdateDto(int id, string nombre, string descripcion, int cantAnios) : base()
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.CantAnios = cantAnios;
        }

    }
}
