using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.DTOs.PropertyDTO
{
    public class GetAllPropertiesDto
    {
       
        public int Id { get; set; }
        public string? Governorate { get; set; } // المحافظة 
        public string? City { get; set; }
        public string? Rooms { get; set; }      
        public double? Area { get; set; }
        public int? Price { get; set; }
        public string? Floor { get; set; }
        public string? MainImage { get; set;}
    }
}
