using CampusVirtual.Data;
using CampusVirtual.Models;
using CampusVirtual.Negocio.Repositorio.Interface;

namespace CampusVirtual.Negocio.Repositorio
{
    public class RolNegocio : BaseNegocio<Rol>, IRolNegocio
    {
        public RolNegocio(Context context) : base(context)
        {
            
        }
    }
}
