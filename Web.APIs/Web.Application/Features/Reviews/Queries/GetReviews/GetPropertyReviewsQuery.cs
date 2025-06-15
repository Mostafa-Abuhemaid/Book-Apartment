using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Reviews.Queries.GetReviews
{
	public record GetPropertyReviewsQuery(int PropertyId) : IRequest<BaseResponse<List<GetPropertyReviewsQueryDto>>>;
}
