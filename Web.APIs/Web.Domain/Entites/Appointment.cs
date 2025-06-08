using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class Appointment:BaseClass<int>
    {

        public int PropertyId { get; set; }
        public Property Property { get; set; } 

        public string UserId { get; set; } 
        public AppUser User { get; set; } 

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime AppointmentDate { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
