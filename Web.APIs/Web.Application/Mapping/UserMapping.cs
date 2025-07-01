using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.PropertyDTO;
using Web.Application.DTOs.UserDTO;
using Web.Domain.Entites;

namespace Web.Application.Mapping
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<AppUser, UserDto>.NewConfig();
                 
        }
    
    
    }
}
