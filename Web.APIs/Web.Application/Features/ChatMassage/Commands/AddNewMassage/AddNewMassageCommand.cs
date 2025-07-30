using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.ChatMassage.Commands.AddNewMassage
{
    public record AddNewMassageCommand(string SenderUserId, string ReceiverUserId, string Content,int ChatId=0) : IRequest<BaseResponse<bool>>;
    
}
