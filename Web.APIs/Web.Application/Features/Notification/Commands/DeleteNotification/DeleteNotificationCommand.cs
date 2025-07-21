using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Notification.Commands.DeleteNotification
{
    public record DeleteNotificationCommand(int Id) : IRequest<BaseResponse<bool>>;
    
}
