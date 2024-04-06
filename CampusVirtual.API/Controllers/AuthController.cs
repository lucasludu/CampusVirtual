using CampusVirtual.API.Util.Interface;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.UOW.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CampusVirtual.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "ApiAuthCV")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioUtil _usuarioUtil;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork, IUsuarioUtil usuarioUtil)
        {
            this._unitOfWork = unitOfWork;
            this._usuarioUtil = usuarioUtil;
        }

        /// <summary>
        /// Registro de un nuevo usuario.
        /// </summary>
        /// <param name="dto">Registro</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [SwaggerOperation(Summary = "Registrar un nuevo usuario")]
        [SwaggerResponse(StatusCodes.Status201Created, "Se registró correctamente el usuario.", typeof(UserRegisterDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No se pudo realizar el registro del usuario.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public ActionResult Register([FromForm] UserRegisterDto dto)
        {
            try
            {
                var usuario = _usuarioUtil.Register(dto, dto.Password);
                var rol = _unitOfWork.Rol.GetByCondition(a => a.Id == dto.IdRol);
                var carrera = _unitOfWork.Carrera.GetByCondition(a => a.Id == dto.IdCarrera);
                usuario.Rol = rol.Nombre;
                usuario.Carrera = carrera.Nombre!;
                return (usuario != null)
                    ? StatusCode(StatusCodes.Status201Created, usuario)
                    : StatusCode(StatusCodes.Status204NoContent, $"No se pudo realizar el registro del usuario {usuario!.Nombre}, {usuario!.Apellido}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Inicio de Sesión
        /// </summary>
        /// <param name="dto">Login</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Iniciar sesión.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Inicio de sesión exitoso.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status203NonAuthoritative, "No se pudo autorizar.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public ActionResult Login([FromForm] UserLoginDto dto)
        {
            try
            {
                var login = _usuarioUtil.Login(dto);
                return (login != null)
                    ? StatusCode(StatusCodes.Status200OK, _usuarioUtil.GetToken(login, login.Rol))
                    : StatusCode(StatusCodes.Status203NonAuthoritative, "No se pudo autorizar.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}