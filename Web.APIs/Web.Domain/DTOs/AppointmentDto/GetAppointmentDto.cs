using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.DTOs.AppointmentDto
{
    public class GetAppointmentDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string? Tilte {  get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string? MainImage { get; set; }      
        public PropertyType PropertyType { get; set; } //بيع او ايجار
        public DateTime CreatedAt { get; set; } 
        public string RequesterId { get; set; }
        public string RequesterName { get; set; }
        public string RequesterPhone { get; set;}
    }
}
