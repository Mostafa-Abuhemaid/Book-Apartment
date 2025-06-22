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
        public int PropertyId { get; set; }
        public string Owner { get; set; }
        public string OwnerPhone { get; set; }
        public string? MainImage { get; set; }      
        public PropertyType PropertyType { get; set; } //بيع او ايجار
        public string? Address { get; set; }
        public string UserId { get; set; }
    }
}
