using AutoMapper;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.UOW.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CampusVirtual.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "ApiUsuarioCV")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        /// <summary>
        /// Devuelve la lista de usuarios (Estudiantes - Admin)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Lista de Usuarios")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de ususarios.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No hay lista de usuarios.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public ActionResult GetAll()
        {
            try
            {
                var lista = _unitOfWork.Usuario.GetAllDto();
                return (lista.Count > 0)
                    ? StatusCode(StatusCodes.Status200OK, lista)
                    : StatusCode(StatusCodes.Status204NoContent, "No hay registros de usuarios");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Devuelve usuario según correo
        /// </summary>
        /// <param name="correo">Correo</param>
        /// <returns></returns>
        [HttpGet("Correo")]
        [SwaggerOperation(Summary = "Usuario según correo")]
        [SwaggerResponse(StatusCodes.Status200OK, "Se encontro el usuario", typeof(UserDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "El usuario a buscar no se encuentra registrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public ActionResult GetByCondition (string correo)
        {
            try
            {
                var usuario = _unitOfWork.Usuario.GetByEmailDto(correo);
                return (usuario != null)
                    ? StatusCode(StatusCodes.Status200OK, usuario)
                    : StatusCode(StatusCodes.Status204NoContent, "No se encontro al usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}