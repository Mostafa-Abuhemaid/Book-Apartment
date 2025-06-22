using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.DTOs.AppointmentDto
{
    public class CreateAppointmentDto
    {
        public int PropertyId { get; set; }
        public string RequesterId { get; set; }
    }
}
