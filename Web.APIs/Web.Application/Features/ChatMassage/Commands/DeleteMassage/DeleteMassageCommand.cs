using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.ChatMassage.Commands.DeleteMassage
{
    public record DeleteMassageCommand(int MessageId ) :IRequest<BaseResponse<bool>>;
   
}
