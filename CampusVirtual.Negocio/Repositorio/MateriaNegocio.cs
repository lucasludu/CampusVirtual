using CampusVirtual.Data;
using CampusVirtual.Models;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.Repositorio.Interface;

namespace CampusVirtual.Negocio.Repositorio
{
    public class MateriaNegocio : BaseNegocio<Materia>, IMateriaNegocio
    {
        public MateriaNegocio(Context context) : base(context)
        {

        }


        /// <summary>
        /// Devuelve el filtro a buscar.
        /// </summary>
        /// <param name="nombre">Filtro Nombre de la materia</param>
        /// <param name="idCarrera">Filtro idcarrera</param>
        /// <returns></returns>
        public List<MateriaDto> GetFiltro(string nombre, int idCarrera)
        {
            var query = from c in _context.Materia select c;

            query = string.IsNullOrEmpty(nombre) ? query : query.Where(a => a.Nombre!.ToLower().Trim().Contains(nombre.ToLower().Trim()));
            query = idCarrera == 0 ? query : query.Where(a => a.IdCarrera == idCarrera);

            var listaDto = (from q in query
                            select new MateriaDto(
                                q.Id, 
                                q.Nombre!, 
                                (int)q.Anio!, 
                                (int)q.Cuatrimestre!, 
                                (int)q.HorasSemanales!, 
                                q.IdCarrera))
                            .ToList();

            return listaDto;
        }


        /// <summary>
        /// Inserta una lista de materias
        /// Si la materia no existe no la ingresa
        /// </summary>
        /// <param name="entities">Lista de materia</param>
        /// <returns></returns>
        public async Task<bool> InsertMasivo(List<Materia> entities)
        {
            using(var transaccion = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach(var entity in entities)
                    {
                        this.Insert(entity);
                    }
                    await transaccion.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await transaccion.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
