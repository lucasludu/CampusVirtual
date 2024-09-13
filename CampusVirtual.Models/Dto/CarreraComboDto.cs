using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Models.Dto
{
    public class CarreraComboDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public CarreraComboDto()
        {
            
        }

        public CarreraComboDto(int id, string nombre) 
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
