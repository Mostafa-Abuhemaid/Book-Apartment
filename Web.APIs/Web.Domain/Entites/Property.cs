using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.Entites;
public class Property:BaseClass<int>
{
   
    public string? Title { get; set; }
    public string? Description { get; set; }
    public PropertyNature Type { get; set; } // شقة، فيلا، شاليه، سطح
    public string? Rooms { get; set; }
    public string? Bathrooms { get; set; }
    public double? Area { get; set; }
    public int? Price { get; set; }
    public string? Floor { get; set; }
    public bool? IsFurnished { get; set; } = false; // مفروشة ولا لاء 
    public PropertyType PropertyType { get; set; } //بيع او ايجار
    public string? Governorate { get; set; } // المحافظة 
    public string? City { get; set; } // المدينة 
    public RentType? RentType { get; set; } // طلابي ولا اسر 
    public double? RentAdvance { get; set; } // مقدم الايجار 
    public double? RentPrice { get; set; }
    public string? PriceRentType { get; set; }// شهري وسنوي 
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public bool? HasWifi { get; set; }
    public bool IsActive {  get; set; }=false;
    public string? MainImage { get; set; }
    public ThereIsInstallment? ThereIsInstallment {  get; set; }
    public string OwnerId { get; set; }
    public AppUser Owner { get; set; }

    public ICollection<PropertyImage>? Images { get; set; } = new List<PropertyImage>();
    public ICollection<PropertyReview>? PropertyReviews { get; set; } = new List<PropertyReview>();


}
