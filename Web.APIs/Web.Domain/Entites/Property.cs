using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.Entites;
public class Property:BaseClass<int>
{
   
    public string Title { get; set; }
    public string Description { get; set; }
    public PropertyNature Type { get; set; } // شقة، فيلا، شاليه، سطح
    public int Rooms { get; set; }
    public int Bathrooms { get; set; }
    public double Area { get; set; }
    public decimal Price { get; set; }
    public int Floor { get; set; }
    public PropertyType PropertyType { get; set; } //بيع او ايجار


   
    public string OwnerId { get; set; }
    public AppUser Owner { get; set; }

    public ICollection<PropertyImage> Images { get; set; }
}
