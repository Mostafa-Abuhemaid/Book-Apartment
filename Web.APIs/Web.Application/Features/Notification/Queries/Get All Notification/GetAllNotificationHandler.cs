using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Reviews.Queries.GetReviews;
using Web.Application.Response;
using Web.Domain.DTOs.NotificationDto;
using Web.Domain.Entites;
using Web.Domain.Repositories;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Notification.Queries.Get_All_Notification
{
    public class GetAllNotificationHandler : IRequestHandler<GetAllNotificationQuery, BaseResponse<List<Notifications>>>
    {

        private readonly AppDbContext _dbContext;
        public GetAllNotificationHandler(IUnitOfWork unitOfWork, AppDbContext dbContext)
        {
       
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<List<Notifications>>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.PageNumber - 1) * request.PageSize;

            var notifications = await _dbContext.Notifications
                .OrderByDescending(n => n.CreatedAt) 
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new BaseResponse<List<Notifications>>(true, "تم الوصول إلى الإشعارات السابقة", notifications);
        }

    }
}
