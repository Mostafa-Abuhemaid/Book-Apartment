using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.ChatMassage.Commands.ReadChatMessages
{
    public record ReadChatMessagesCommand(int ChatId):IRequest<BaseResponse<bool>>;
  
}
