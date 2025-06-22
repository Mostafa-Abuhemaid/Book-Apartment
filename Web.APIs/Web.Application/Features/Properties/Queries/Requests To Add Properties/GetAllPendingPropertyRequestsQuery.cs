using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;

namespace Web.Application.Features.Properties.Queries.Requests_To_Add_Properties
{
    public class GetAllPendingPropertyRequestsQuery : IRequest<BaseResponse<List<RequestsToAddPropertiesDto>>>
    {

    }
}
