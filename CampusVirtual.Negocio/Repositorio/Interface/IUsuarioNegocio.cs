using CampusVirtual.Models;
using CampusVirtual.Models.Dto;

namespace CampusVirtual.Negocio.Repositorio.Interface
{
    public interface IUsuarioNegocio : IBaseNegocio<Usuario>
    {
        List<UserDto> GetAllDto();
        UserDto GetByEmailDto(string email);  
    }
}
