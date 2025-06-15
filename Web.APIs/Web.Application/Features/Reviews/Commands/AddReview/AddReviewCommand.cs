using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.Entites;

namespace Web.Application.Features.Reviews.Commands.AddReview
{
	public record AddReviewCommand(int PropertyId,string Review,int Stars):IRequest<BaseResponse<List<string>>>;
}
