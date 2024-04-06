using CampusVirtual.Models;
using CampusVirtual.Models.Dto;

namespace CampusVirtual.Negocio.Repositorio.Interface
{
    public interface ICarreraNegocio : IBaseNegocio<Carrera>
    {
        Task<bool> InsertMasivo(List<Carrera> entities);
        List<CarreraDto> GetFiltro(string nombre);
    }
}
