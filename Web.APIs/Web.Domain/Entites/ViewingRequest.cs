using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class ViewingRequest:BaseClass<int>
    {

        public int PropertyId { get; set; }
        public Property Property { get; set; } 

        public string RequesterId { get; set; } 
        public AppUser Requester { get; set; } 

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
       
        public bool IsConfirmed { get; set; } = false;
    }
}
