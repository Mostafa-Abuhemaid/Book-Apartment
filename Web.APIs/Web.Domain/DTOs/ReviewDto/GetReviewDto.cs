using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.DTOs.ReviewDto
{
    public class GetReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }      
        public DateTime CreatedAt { get; set; }
    
    }
}
