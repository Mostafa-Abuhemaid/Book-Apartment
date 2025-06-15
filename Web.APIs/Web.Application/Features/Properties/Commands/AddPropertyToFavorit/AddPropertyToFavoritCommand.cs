using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Properties.Commands.AddPropertyToFavorit
{
	public record AddPropertyToFavoritCommand(int PropertyId):IRequest<BaseResponse<int>>;
	
}
