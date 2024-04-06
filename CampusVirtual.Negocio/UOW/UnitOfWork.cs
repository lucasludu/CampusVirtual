using CampusVirtual.Data;
using CampusVirtual.Negocio.Repositorio;
using CampusVirtual.Negocio.Repositorio.Interface;
using CampusVirtual.Negocio.UOW.Interface;

namespace CampusVirtual.Negocio.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly Context _context;

        public IUsuarioNegocio Usuario { get; private set; } 
        public IMateriaNegocio Materia { get; private set; }
        public ICarreraNegocio Carrera { get; private set; }
        public IRolNegocio     Rol{ get; private set; }

        public UnitOfWork(Context context)
        {
            this._context = context;
            this.Usuario  = new UsuarioNegocio(context);
            this.Materia  = new MateriaNegocio(context);
            this.Carrera  = new CarreraNegocio(context);
            this.Rol      = new RolNegocio(context);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
