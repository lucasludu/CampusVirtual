using CampusVirtual.Data;
using CampusVirtual.Models;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.Repositorio.Interface;

namespace CampusVirtual.Negocio.Repositorio
{
    public class CarreraNegocio : BaseNegocio<Carrera>, ICarreraNegocio
    {
        public CarreraNegocio(Context context) : base(context)
        {
        }


        /// <summary>
        /// Devuelve el filtro a buscar.
        /// </summary>
        /// <param name="nombre">Filtro Nombre de la carrera</param>
        /// <returns></returns>
        public List<CarreraDto> GetFiltro(string nombre)
        {
            var query = from c in _context.Carreras select c;

            query = string.IsNullOrEmpty(nombre) ? query : query.Where(a => a.Nombre!.ToLower().Trim().Contains(nombre.ToLower().Trim()));

            var listDto = (from q in query
                           select new CarreraDto(
                               q.Id, q.Nombre!, q.Descripcion!, (int)q.CantAnios!
                           )).ToList();

            return listDto;
        }

        /// <summary>
        /// Inserta una lista de carreras
        /// Si la carrera no existe no la ingresa
        /// </summary>
        /// <param name="entities">Lista de carrera</param>
        /// <returns></returns>
        public async Task<bool> InsertMasivo(List<Carrera> entities)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        this.Insert(entity);
                    }

                    transaction.Commit(); // Finalizamos la transacción
                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Si hay un error, revertimos la transacción
                    return false;
                }
            }
        }
   
    }
}
