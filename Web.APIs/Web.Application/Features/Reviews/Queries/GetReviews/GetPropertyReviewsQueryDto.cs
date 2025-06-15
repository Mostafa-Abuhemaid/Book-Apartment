using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Reviews.Queries.GetReviews
{
	public class GetPropertyReviewsQueryDto
	{
		public int Id { get; set; }
		public string Comment { get; set; }
		public int Stars { get; set; }
		public DateTime CreatedAt { get; set; }
		public ReviewerDto reviewer { get; set; }
	}
}
