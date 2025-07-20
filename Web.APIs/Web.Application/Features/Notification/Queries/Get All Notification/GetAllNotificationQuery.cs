using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Reviews.Queries.GetReviews;
using Web.Application.Response;
using Web.Domain.DTOs.NotificationDto;
using Web.Domain.Entites;

namespace Web.Application.Features.Notification.Queries.Get_All_Notification
{
    public record GetAllNotificationQuery(int PageNumber,int PageSize) : IRequest<BaseResponse<List<Notifications>>>;
   
}
