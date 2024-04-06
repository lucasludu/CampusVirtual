using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Models.Dto
{
    public class UserLoginDto
    {
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserLoginDto()
        {
            
        }

        public UserLoginDto(string correo, string password) : base()
        {
            this.Correo = correo;
            this.Password = password;
        }
    }
}
