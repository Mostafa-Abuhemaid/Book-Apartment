using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Reviews.Commands.UpdateReview
{
	public record UpdateReviewCommand(int Id, string Comment,int Stars):IRequest<BaseResponse<List<string>>>;
	
}
