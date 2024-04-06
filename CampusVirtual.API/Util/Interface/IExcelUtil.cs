using CampusVirtual.Models.Dto;

namespace CampusVirtual.API.Util.Interface
{
    public interface IExcelUtil
    {
        Task<List<CarreraInsertDto>> LeerCarreraExcel(IFormFile file);   
        Task<List<MateriaInsertDto>> LeerMateriaExcel(IFormFile file);   
    }
}
