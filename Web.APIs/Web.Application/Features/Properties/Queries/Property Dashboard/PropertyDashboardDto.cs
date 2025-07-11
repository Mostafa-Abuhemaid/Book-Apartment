using Mapster.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Queries.Property_Dashboard
{
    public class PropertyDashboardDto(  )
    {
    public  int Id { get; set; }
    public  string? Governorate   {get;set;}
    public  string? City          {get;set;}
    public  DateTime CreatedAt    {get;set;}
    public  string OwnerName    {get;set;}
    public  PropertySaleStatus? PropertySaleStatus    {get;set;}
    public PropertyRentStatus? PropertyRentStatus { get; set; }
    
    }
   
      
    
}
