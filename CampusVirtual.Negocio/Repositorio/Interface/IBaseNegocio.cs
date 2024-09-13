using System.Linq.Expressions;

namespace CampusVirtual.Negocio.Repositorio.Interface
{
    public interface IBaseNegocio<T> where T : class
    {
        T Get(int id);
        T GetByCondition(Expression<Func<T, bool>> condition);
        List<T> GetAll();
        List<T> GetAllByCondition(Expression<Func<T, bool>> condition);
        bool Exists(Expression<Func<T, bool>> expression);

        bool SaveChanges();
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
