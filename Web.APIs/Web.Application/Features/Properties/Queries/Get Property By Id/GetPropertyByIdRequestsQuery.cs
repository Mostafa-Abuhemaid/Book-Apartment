using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;

namespace Web.Application.Features.Properties.Queries.Get_Property_By_Id
{
    public record GetPropertyByIdRequestsQuery(int PropertyId) : IRequest<BaseResponse<GetPropertyDto>>
    {


    }
}
