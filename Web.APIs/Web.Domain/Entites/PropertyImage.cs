using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites;
public class PropertyImage : BaseClass<int>
{

    public string? ImageUrl { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
}

