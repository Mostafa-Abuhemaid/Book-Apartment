using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class Notifications:BaseClass<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
