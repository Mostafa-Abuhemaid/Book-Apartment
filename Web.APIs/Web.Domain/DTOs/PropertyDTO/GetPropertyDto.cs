using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.DTOs.PropertyDTO
{
    public class GetPropertyDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public PropertyNature Type { get; set; } // شقة، فيلا، شاليه، سطح
        public string? Rooms { get; set; }
        public string? Bathrooms { get; set; }
        public double? Area { get; set; }
        public int? Price { get; set; }
        public string? Floor { get; set; }
        public PropertyType PropertyType { get; set; } //بيع او ايجار
        public string? Governorate { get; set; } // المحافظة 
        public string? City { get; set; } // المدينة 
        public RentType? RentType { get; set; } // طلابي ولا اسر 
        public bool? IsFurnished { get; set; }  // مفروشة ولا لاء 
        public double? RentAdvance { get; set; } // مقدم الايجار 
        public double? RentPrice { get; set; }
        public string? PriceRentType { get; set; }// شهري وسنوي 
        public AvailabilityStatus? AvailabilityStatus { get; set; }
        public bool? HasWifi { get; set; }
        public bool IsActive { get; set; } = false;
        public string? MainImage { get; set; }
         
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerImage { get; set; }
        public ICollection<string>? Images { get; set; } = new List<string>();
      
    }
}
