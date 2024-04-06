using AutoMapper;
using CampusVirtual.Models.Dto;

namespace CampusVirtual.Models.Mapper
{
    public class MapperCV : Profile
    {
        public MapperCV()
        {
            CreateMap<Usuario, UserDto>().ReverseMap();
            CreateMap<Usuario, UserRegisterDto>().ReverseMap();
            CreateMap<Usuario, UserLoginDto>().ReverseMap();
            
            CreateMap<Materia, MateriaDto>().ReverseMap();
            CreateMap<Materia, MateriaInsertDto>().ReverseMap();
            
            CreateMap<Carrera, CarreraInsertDto>().ReverseMap();
            CreateMap<Carrera, CarreraUpdateDto>().ReverseMap();
            CreateMap<Carrera, CarreraDto>().ReverseMap();

        }
    }
}
