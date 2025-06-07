using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class UserFavorite
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
