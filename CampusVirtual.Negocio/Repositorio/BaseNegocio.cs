using CampusVirtual.Data;
using CampusVirtual.Negocio.Repositorio.Interface;
using System.Linq.Expressions;

namespace CampusVirtual.Negocio.Repositorio
{
    public class BaseNegocio<T> : IBaseNegocio<T> where T : class
    {
        protected readonly Context _context;

        public BaseNegocio(Context context)
        {
            this._context = context;
        }

        public bool Delete(T entity)
        {
            this._context!.Set<T>().Remove(entity);
            return this.SaveChanges();
        }

        public T Get(int id)
        {
            return this._context!.Set<T>().Find(id)!;
        }

        public List<T> GetAll()
        {
            return this._context!.Set<T>().ToList();
        }

        public List<T> GetAllByCondition(Expression<Func<T, bool>> condition)
        {
            return this._context!.Set<T>().Where(condition).ToList();
        }

        public T GetByCondition(Expression<Func<T, bool>> condition)
        {
            return this._context!.Set<T>().Where(condition).FirstOrDefault()!;
        }

        public bool Insert(T entity)
        {
            this._context!.Set<T>().Add(entity); 
            return this.SaveChanges();
        }

        public bool SaveChanges()
        {
            return this._context!.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            this._context!.Set<T>().Update(entity); 
            return this.SaveChanges();
        }
    }
}
