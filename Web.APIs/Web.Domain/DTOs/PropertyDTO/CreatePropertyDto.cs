using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;
using Web.Domain.Enums;

namespace Web.Application.DTOs.PropertyDTO
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public PropertyNature Type { get; set; } // شقة، فيلا، شاليه، سطح
        public int? Rooms { get; set; }
        public int? Bathrooms { get; set; }
        public double? Area { get; set; }
        public int Price { get; set; }
        public int? Floor { get; set; }
        public PropertyType PropertyType { get; set; } //بيع او ايجار
        public string? Address { get; set; }
        public RentType? RentType { get; set; }  // اسر او طلاب
        public AvailabilityStatus? AvailabilityStatus { get; set; }
        public bool? HasWifi { get; set; }
        public IFormFile? MainImage { get; set; }
        public string OwnerId { get; set; }       
        public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();
       
    }
}
