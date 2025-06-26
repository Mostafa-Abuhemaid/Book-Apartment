using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Properties.Commands.ApproveProperty
{
    public record ApprovePropertyCommand(int PropertyId) : IRequest<BaseResponse<bool>>
    {
    }
}
