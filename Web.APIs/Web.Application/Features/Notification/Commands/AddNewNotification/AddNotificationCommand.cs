using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;

namespace Web.Application.Features.Notification.Commands.AddNewNotification
{
    public record AddNotificationQuery
    (string Title ,string Description) : IRequest<BaseResponse<string>>;
}
