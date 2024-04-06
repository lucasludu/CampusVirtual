namespace CampusVirtual.Models.Dto
{
    public class MateriaDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? Anio { get; set; }
        public int? Cuatrimestre { get; set; }
        public int? HorasSemanales { get; set; }
        public int? IdCarrera { get; set; }

        public MateriaDto()
        {
            
        }

        public MateriaDto(int id, string nombre, int anio, int cuatrimestre, int horassemanales, int idcarrera) : base()
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Anio = anio;
            this.Cuatrimestre = cuatrimestre;
            this.HorasSemanales = horassemanales;
            this.IdCarrera = idcarrera;
        }
    }
}
