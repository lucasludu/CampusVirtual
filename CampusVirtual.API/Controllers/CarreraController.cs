using AutoMapper;
using CampusVirtual.API.Util.Interface;
using CampusVirtual.Models;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.UOW.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
    /// <summary>
    /// CONTROLADOR DE LA CLASE CARRERA
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "ApiCarreraCV")]
    public class CarreraController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExcelUtil _excelUtil;

        public CarreraController(IUnitOfWork unitOfWork, IMapper mapper, IExcelUtil excelUtil)
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
                var listaCarreraDto = _excelUtil.LeerCarreraExcel(file);

                // Al no soportar mappeo se hace de esta forma.
                var listaCarrera = new List<Carrera>();
                foreach(var carrera in await listaCarreraDto)
                {
                    if(_unitOfWork.Carrera.GetByCondition(a => a.Nombre!.ToLower().Trim().Equals(carrera.Nombre!.ToLower().Trim())) == null)
                    {
                        var carreraDto = new Carrera();
                        carreraDto.Nombre = carrera.Nombre;
                        carreraDto.Descripcion = carrera.Descripcion;
                        carreraDto.CantAnios = carrera.CantAnios;
                        listaCarrera.Add(carreraDto);
                    }
                }

                return await _unitOfWork.Carrera.InsertMasivo(listaCarrera)
                    ? StatusCode(StatusCodes.Status200OK, "Se insertó la lista de carreras")
                    : StatusCode(StatusCodes.Status400BadRequest, "No se pudo ingresar la lista de carreras");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Inserta una carrera
        /// </summary>
        /// <param name="carreraInsertDto">Dto Insert</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("carreraInsertDto")]
        public ActionResult Insert([FromForm] CarreraInsertDto carreraInsertDto) 
        {
            try
            {
                var carrera = _unitOfWork.Carrera.GetByCondition(a => a.Nombre!.ToLower().Trim().Equals(carreraInsertDto.Nombre!.ToLower().Trim()));

                return carrera == null
                    ? (_unitOfWork.Carrera.Insert(_mapper.Map<Carrera>(carreraInsertDto)))
                        ? StatusCode(StatusCodes.Status201Created, $"Se ingreso una nueva carrera: {carreraInsertDto.Nombre}")
                        : StatusCode(StatusCodes.Status400BadRequest, "No se pudo ingresar la nueva carrera.")
                    : StatusCode(StatusCodes.Status400BadRequest, "La carrera ya se encuentra registrada");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Devuelve una carrera según nombre
        /// </summary>
        /// <param name="condition">Condición</param>
        /// <returns></returns>
        [HttpGet("condition")]
        public ActionResult GetByCondition(string condition) 
        {
            try
            {
                var carrera = _unitOfWork.Carrera.GetByCondition(a => a.Nombre!.ToLower().Trim().Equals(condition.ToLower().Trim()));

                return carrera != null
                    ? StatusCode(StatusCodes.Status200OK, _mapper.Map<CarreraDto>(carrera))
                    : StatusCode(StatusCodes.Status204NoContent, "No se encuentra la carrera ingresada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Devuelve una lista de Carrera
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll() 
        {
            try
            {
                var list = _unitOfWork.Carrera.GetAll();

                return list.Count != 0
                    ? StatusCode(StatusCodes.Status200OK, _mapper.Map<List<CarreraDto>>(list))
                    : StatusCode(StatusCodes.Status204NoContent, "No se encuentra la carrera ingresada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Filtra la carrera según el nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public ActionResult GetByFilter(string nombre) 
        {
            try
            {
                var lista = _unitOfWork.Carrera.GetFiltro(nombre);
                return (lista.Count != 0)
                    ? StatusCode(StatusCodes.Status200OK, lista)
                    : StatusCode(StatusCodes.Status204NoContent, "No hay lista cargada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Se modifica la carrera a buscar. El ID tiene que conincidir con el ID de la carreraDto
        /// </summary>
        /// <param name="id">ID de la carrera a buscar</param>
        /// <param name="carreraDto">Carrera a modificar</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]
        public ActionResult Put(int id, [FromForm] CarreraUpdateDto carreraDto) 
        {
            try
            {
                var existeCarrera = _unitOfWork.Carrera.GetByCondition(a => a.Id == id);

                if(existeCarrera.Id == carreraDto.Id)
                {
                    if (!string.IsNullOrEmpty(carreraDto.Nombre))
                    {
                        existeCarrera.Nombre = carreraDto.Nombre;
                    }
                    if (!string.IsNullOrEmpty(carreraDto.Descripcion))
                    {
                        existeCarrera.Descripcion = carreraDto.Descripcion;
                    }
                    if (carreraDto.CantAnios.HasValue)
                    {
                        existeCarrera.CantAnios = carreraDto.CantAnios.Value;
                    }

                    var carrera = _mapper.Map<Carrera>(existeCarrera);
                    return _unitOfWork.Carrera.Update(carrera)
                        ? StatusCode(StatusCodes.Status200OK, _mapper.Map<CarreraDto>(carrera))
                        : StatusCode(StatusCodes.Status400BadRequest, "No se pudo modificar");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "La carrera no existe.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Elimina la carrera a buscar.
        /// </summary>
        /// <param name="id">Id de la carrera a buscar y eliminar</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public ActionResult Delete(int id) 
        {
            try
            {
                var carreraDelete = _unitOfWork.Carrera.GetByCondition(a => a.Id == id);

                return carreraDelete != null
                    ? _unitOfWork.Carrera.Delete(carreraDelete)
                        ? StatusCode(StatusCodes.Status200OK, $"Carrera {carreraDelete.Nombre} borrada con éxito")
                        : StatusCode(StatusCodes.Status404NotFound, "No se pudo borrar la carrera.")
                    : StatusCode(StatusCodes.Status204NoContent, "La carrera no se encuentra registrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
  
    }
}