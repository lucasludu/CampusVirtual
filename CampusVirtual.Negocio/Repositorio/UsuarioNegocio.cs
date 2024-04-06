using CampusVirtual.Data;
using CampusVirtual.Models;
using CampusVirtual.Models.Dto;
using CampusVirtual.Negocio.Repositorio.Interface;

namespace CampusVirtual.Negocio.Repositorio
{
    public class UsuarioNegocio : BaseNegocio<Usuario>, IUsuarioNegocio
    {
        public UsuarioNegocio(Context context) : base(context)
        {
            
        }

        public List<UserDto> GetAllDto()
        {
            var query = (from usuario in _context.Usuarios
                    join rol in _context.Rols on usuario.IdRol equals rol.Id
                    join carrera in _context.Carreras on usuario.IdCarrera equals carrera.Id
                    select new UserDto
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Dni = usuario.Dni,
                        Correo = usuario.Correo,
                        Carrera = carrera.Nombre!,
                        Rol = rol.Nombre
                    });

            var querySinCarrera = (from usuario in _context.Usuarios
                                   join rol in _context.Rols on usuario.IdRol equals rol.Id
                                   join carrera in _context.Carreras on usuario.IdCarrera equals carrera.Id into carreras
                                   from carreraLeft in carreras.DefaultIfEmpty()
                                   select new UserDto
                                   {
                                       Id = usuario.Id,
                                       Nombre = usuario.Nombre,
                                       Apellido = usuario.Apellido,
                                       Dni = usuario.Dni,
                                       Correo = usuario.Correo,
                                       Carrera = carreraLeft != null ? carreraLeft.Nombre! : null!,
                                       Rol = rol.Nombre
                                   });

            return query.Union(querySinCarrera).ToList();
        }

        public UserDto GetByEmailDto(string email)
        {
            var query = (from usuario in _context.Usuarios
                    join rol in _context.Rols on usuario.IdRol equals rol.Id
                    join carrera in _context.Carreras on usuario.IdCarrera equals carrera.Id
                    where usuario.Correo.Equals(email)
                    select new UserDto
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Dni = usuario.Dni,
                        Correo = usuario.Correo,
                        Carrera = carrera.Nombre!,
                        Rol = rol.Nombre
                    });

            var querySinCarrera = (from usuario in _context.Usuarios
                                  join rol in _context.Rols on usuario.IdRol equals rol.Id
                                  join carrera in _context.Carreras on usuario.IdCarrera equals carrera.Id into carreras
                                  from carreraLeft in carreras.DefaultIfEmpty()
                                  where usuario.Correo.Equals(email)
                                  select new UserDto
                                  {
                                      Id = usuario.Id,
                                      Nombre = usuario.Nombre,
                                      Apellido = usuario.Apellido,
                                      Dni = usuario.Dni,
                                      Correo = usuario.Correo,
                                      Carrera = carreraLeft != null ? carreraLeft.Nombre! : null!,
                                      Rol = rol.Nombre
                                  });

            return query.Union(querySinCarrera).FirstOrDefault()!;
        }
    }
}
