using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Properties.Queries.GetFavorit
{
	public record GetFavoritQuery(string userId):IRequest<BaseResponse<List<GetFavoritQueryDto>>>;
	
}
