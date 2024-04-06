using AutoMapper;
using CampusVirtual.API.Util.Interface;
using CampusVirtual.Models;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.UOW.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CampusVirtual.API.Util
{
    public class UsuarioUtil : IUsuarioUtil
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioUtil(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
        {
            this._uow = uow;
            _mapper = mapper;
            _configuration = configuration;
        }


        public string GetToken(UserDto userDto, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userDto.Nombre),
                new Claim(ClaimTypes.Name, userDto.Apellido),
                new Claim(ClaimTypes.Name, userDto.Dni),
                new Claim(ClaimTypes.Email, userDto.Correo),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(120),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hMac = new HMACSHA512(passwordSalt);
            var hash = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (var i = 0; i < hash.Length; i++)
            {
                if (hash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }


        private void CrearPassHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hMac = new HMACSHA512();
            passwordSalt = hMac.Key;
            passwordHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }



        public UserDto Login(UserLoginDto userLoginDto)
        {
            var usuario = _uow.Usuario.GetByCondition(a => a.Correo.Equals(userLoginDto.Correo));
            var rol = _uow.Rol.GetByCondition(a => a.Id == usuario.Id);
            var userDto = _mapper.Map<UserDto>(usuario);
            userDto.Rol = rol.Nombre;
            if (usuario != null)
            {
                return (!VerifyPassword(userLoginDto.Password, usuario.PasswordHash, usuario.PasswordSalt))
                    ? null!
                    : userDto;
            }
            return null!;
        }


        public UserDto Register(UserRegisterDto userRegisterDto, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CrearPassHash(password, out passwordHash, out passwordSalt);
            var user = _mapper.Map<Usuario>(userRegisterDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (user.IdRol != 1 && user.IdRol != 2)
                throw new ArgumentException("El Rol tiene que ser Admin o Alumno");

            _uow.Usuario.Insert(user);

            return _mapper.Map<UserDto>(user);
        }
   
    }
}
