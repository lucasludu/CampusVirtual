using CampusVirtual.Negocio.Repositorio.Interface;

namespace CampusVirtual.Negocio.UOW.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioNegocio Usuario { get; }
        IMateriaNegocio Materia { get; }
        ICarreraNegocio Carrera { get; }
        IRolNegocio     Rol{ get; }
    }
}
