using CampusVirtual.Models.Dto;

namespace CampusVirtual.API.Util.Interface
{
    public interface IUsuarioUtil
    {
        UserDto Register (UserRegisterDto userRegisterDto, string password);
        UserDto Login(UserLoginDto userLoginDto);
        string GetToken(UserDto userDto, string role);
    }
}
