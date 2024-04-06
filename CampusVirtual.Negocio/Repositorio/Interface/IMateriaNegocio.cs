using CampusVirtual.Models;
using CampusVirtual.Models.Dto;

namespace CampusVirtual.Negocio.Repositorio.Interface
{
    public interface IMateriaNegocio : IBaseNegocio<Materia>
    {
        Task<bool> InsertMasivo(List<Materia> entities);
        List<MateriaDto> GetFiltro(string nombre, int idMateria);
    }
}
