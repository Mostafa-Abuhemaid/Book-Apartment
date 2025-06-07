using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Application.DTOs.PropertyDTO
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }

       
        public string? Description { get; set; }

        public decimal Price { get; set; }

       
        public PropertyType Type { get; set; }

        public PropertyNature PropertyNature { get; set; }

     
        public int Bedrooms { get; set; }

      
        public int Bathrooms { get; set; }

    
        public double Area { get; set; }

   
        public string? Address { get; set; }

        public List<string> Images { get; set; } = new List<string>();
    }
}
