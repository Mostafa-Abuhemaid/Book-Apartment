using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.DTOs.AccountDTO
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
       
        public Roles Role {  get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
}
