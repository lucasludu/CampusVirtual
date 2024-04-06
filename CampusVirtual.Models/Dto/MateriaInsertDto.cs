namespace CampusVirtual.Models.Dto
{
    public class MateriaInsertDto
    {
        public string? Nombre { get; set; }
        public int? Anio { get; set; }
        public int? Cuatrimestre { get; set; }
        public int? HorasSemanales { get; set; }
        public int IdCarrera { get; set; }

        public MateriaInsertDto()
        {
            
        }

        public MateriaInsertDto(string nombre, int anio, int cuatrimestre, int horassemanales, int idcarrera) : base()
        {
            this.Nombre = nombre;
            this.Anio = anio;
            this.Cuatrimestre = cuatrimestre;
            this.HorasSemanales = horassemanales;
            this.IdCarrera = idcarrera;
        }
    }
}
