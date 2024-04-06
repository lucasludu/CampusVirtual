using CampusVirtual.API.Util.Interface;
using CampusVirtual.Models.Dto;
using ClosedXML.Excel;

namespace CampusVirtual.API.Util
{
    public class ExcelUtil : IExcelUtil
    {

        /// <summary>
        /// Lee archivo de excel con lista de Carreras
        /// </summary>
        /// <param name="file">Archivo</param>
        /// <returns>Lista de carrera</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<CarreraInsertDto>> LeerCarreraExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Archivo no proporcionado.");

            var stringFileName = Path.GetFileNameWithoutExtension(file.FileName);

            if (!stringFileName.Contains("Carreras", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("El archivo tiene que ser de la carrera");

            if (!file.FileName.EndsWith(".xlsx"))
                throw new ArgumentException("El archivo debe ser de formato Excel (.xlsx)");

            var listaCarrera = new List<CarreraInsertDto>();
            using (var stream = file.OpenReadStream())
            {
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Ignora la primera fila (encabezados)

                    foreach(var row in rows)
                    {
                        var carreraDto = new CarreraInsertDto();
                        carreraDto.Nombre = row.Cell(1).Value.ToString();
                        carreraDto.Descripcion = row.Cell(2).Value.ToString();
                        carreraDto.CantAnios = (int)row.Cell(3).Value;

                        listaCarrera.Add(carreraDto);
                    }
                }
            }

            return listaCarrera;
        }


        /// <summary>
        /// Lee archivo de excel con lista de Materias
        /// </summary>
        /// <param name="file">Archivo</param>
        /// <returns>Lista de materias</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<MateriaInsertDto>> LeerMateriaExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Archivo no proporcionado.");

            var stringFileName = Path.GetFileNameWithoutExtension(file.FileName);

            if (!stringFileName.Contains("Materia", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("El archivo tiene que ser de materia");

            if (!file.FileName.EndsWith(".xlsx"))
                throw new ArgumentException("El archivo debe ser de formato Excel (.xlsx)");

            var listaMateria = new List<MateriaInsertDto>();
            using (var stream = file.OpenReadStream())
            {
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Ignora la primera fila (encabezados)

                    foreach (var row in rows)
                    {
                        var materiaDto = new MateriaInsertDto();
                        materiaDto.Nombre = row.Cell(1).Value.ToString();
                        materiaDto.Anio = (int)row.Cell(2).Value;
                        materiaDto.Cuatrimestre = (int)row.Cell(3).Value;
                        materiaDto.HorasSemanales = (int)row.Cell(4).Value;
                        materiaDto.IdCarrera = (int)row.Cell(5).Value;

                        listaMateria.Add(materiaDto);
                    }
                }
            }
            return listaMateria;
        }
    }
}
