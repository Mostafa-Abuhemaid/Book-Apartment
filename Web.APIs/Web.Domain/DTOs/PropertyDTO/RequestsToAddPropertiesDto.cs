using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.DTOs.PropertyDTO
{
    public class RequestsToAddPropertiesDto
    {
        public int Id { get; set; }
        public string? Governorate { get; set; } 
        public string City { get; set; }    
        public string UserFullName { get; set; }
        public string UserImage { get; set; }
        public PropertyType PropertyType { get; set; }
        public string PropertyMainImage { get; set; }
    }
}
