using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Features.Reviews.Queries.GetReviews
{
	public record ReviewerDto(string Id,string UserName,string? ProfileImage);
	
}
