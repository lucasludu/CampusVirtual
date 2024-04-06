using AutoMapper;
using CampusVirtual.API.Util.Interface;
using CampusVirtual.Models;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.UOW.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "ApiMateriaCV")]
    public class MateriaController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IExcelUtil _excelUtil;

        public MateriaController(IUnitOfWork unitOfWork, IMapper mapper, IExcelUtil excelUtil)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._excelUtil = excelUtil;
        }

        /// <summary>
        /// Inserta masivamente desde un excel
        /// </summary>
        /// <param name="file">Archivo Excel (.xlsx)</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("file")]
        public async Task<ActionResult> InsertByFile(IFormFile file) 
        {
            try
            {
                var listaMateriasDto = _excelUtil.LeerMateriaExcel(file);
                var listaMateria = new List<Materia>();

                foreach(var materiaDto in await listaMateriasDto)
                {
                    if (_unitOfWork.Materia.GetByCondition(a => a.Nombre!.ToLower().Trim().Equals(materiaDto.Nombre!.ToLower().Trim())) == null)
                    {
                        var materia = new Materia();
                        materia.Nombre = materiaDto.Nombre;
                        materia.Anio = materiaDto.Anio;
                        materia.Cuatrimestre = materiaDto.Cuatrimestre;
                        materia.HorasSemanales = materiaDto.HorasSemanales;
                        materia.IdCarrera = materiaDto.IdCarrera;

                        listaMateria.Add(materia);
                    }
                }

                return await _unitOfWork.Materia.InsertMasivo(listaMateria)
                    ? StatusCode(StatusCodes.Status201Created, "Se ha insertado la lista de materias.")
                    : StatusCode(StatusCodes.Status400BadRequest, "No se ha podido insertar el archivo .xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Inserta una Materia
        /// </summary>
        /// <param name="materiaInsertDto">Dto Insert</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("insert")]
        public ActionResult Insert([FromForm] MateriaInsertDto materiaInsertDto) 
        {
            try
            {
                var existeMateria = _unitOfWork.Materia.GetByCondition(a => a.Nombre!.ToLower().Trim().Equals(materiaInsertDto.Nombre!.ToLower().Trim()));

                return existeMateria == null
                    ? _unitOfWork.Materia.Insert(_mapper.Map<Materia>(materiaInsertDto))
                        ? StatusCode(StatusCodes.Status201Created, materiaInsertDto)
                        : StatusCode(StatusCodes.Status404NotFound, "No se pudo ingresar la materia")
                    : StatusCode(StatusCodes.Status400BadRequest, "La materia a ingresar ya se encuentra en el registro.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Devuelve una lista de Materia
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll() 
        {
            try
            {
                var lista = _unitOfWork.Materia.GetAll();

                return lista.Count > 0
                    ? StatusCode(StatusCodes.Status200OK, _mapper.Map<List<MateriaDto>>(lista))
                    : StatusCode(StatusCodes.Status204NoContent, "No hay lista cargada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Devuelve una materia según nombre
        /// </summary>
        /// <param name="condition">Condición</param>
        /// <returns></returns>
        [HttpGet("condition")]
        public ActionResult GetByCondition (string condition) 
        {
            try
            {
                var materia = _unitOfWork.Materia.GetByCondition(a => a.Nombre!.ToLower().Trim().Equals(condition.ToLower().Trim()));

                return materia != null
                    ? StatusCode(StatusCodes.Status200OK, _mapper.Map<MateriaDto>(materia))
                    : StatusCode(StatusCodes.Status204NoContent, "No se encuentra la materia.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Filtra la materia según el nombre y idcarrera
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idCarrera"></param>
        /// <returns></returns>
        [HttpGet("filtro")]
        public ActionResult GetByFiltro(string nombre, int idCarrera) 
        {
            try
            {
                var lista = _unitOfWork.Materia.GetFiltro(nombre, idCarrera);

                return lista.Count > 0
                    ? StatusCode(StatusCodes.Status200OK, _mapper.Map<List<MateriaDto>>(lista))
                    : StatusCode(StatusCodes.Status204NoContent, "No hay registros");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Se modifica la materia a buscar. El ID tiene que conincidir con el ID de la materiaDto
        /// </summary>
        /// <param name="id">ID de la carrera a buscar</param>
        /// <param name="materiaDto">Materia a modificar</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]
        public ActionResult Put(int id, [FromForm] MateriaDto materiaDto) 
        {
            try
            {
                var existeMateria = _unitOfWork.Materia.GetByCondition(a => a.Id == id);

                if (existeMateria.Id == id)
                {
                    if (!string.IsNullOrEmpty(materiaDto.Nombre))
                    {
                        existeMateria.Nombre = materiaDto.Nombre;
                    }
                    if (materiaDto.Anio.HasValue)
                    {
                        existeMateria.Anio = materiaDto.Anio.Value;
                    }
                    if (materiaDto.Cuatrimestre.HasValue)
                    {
                        existeMateria.Cuatrimestre = materiaDto.Cuatrimestre.Value;
                    }
                    if (materiaDto.HorasSemanales.HasValue)
                    {
                        existeMateria.HorasSemanales = materiaDto.HorasSemanales.Value;
                    }
                    if (materiaDto.IdCarrera.HasValue)
                    {
                        existeMateria.IdCarrera = materiaDto.IdCarrera.Value;
                    }

                    var materia = _mapper.Map<Materia>(materiaDto);

                    return _unitOfWork.Materia.Update(materia)
                        ? StatusCode(StatusCodes.Status200OK, _mapper.Map<MateriaDto>(materia))
                        : StatusCode(StatusCodes.Status400BadRequest, "No se pudo modificar la materia");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No se encuentra la materia ingresada");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Elimina la materia a buscar.
        /// </summary>
        /// <param name="id">Id de la materia a buscar y eliminar</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public ActionResult Delete(int id) 
        {
            try
            {
                var materiaDelete = _unitOfWork.Materia.GetByCondition(a => a.Id == id);

                return materiaDelete != null
                    ? _unitOfWork.Materia.Delete(materiaDelete)
                        ? StatusCode(StatusCodes.Status200OK, $"Materia {materiaDelete.Nombre} borrada con éxito")
                        : StatusCode(StatusCodes.Status404NotFound, "No se pudo borrar la materia.")
                    : StatusCode(StatusCodes.Status204NoContent, "La materia no se encuentra registrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}