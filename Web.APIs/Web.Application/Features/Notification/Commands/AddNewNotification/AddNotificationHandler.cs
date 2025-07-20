
using MediatR;
using Web.Application.Response;
using Web.Domain.Repositories;
using Web.Domain.Entites;
using FirebaseAdmin.Messaging;
using Web.Domain.DTOs.NotificationDto;
namespace Web.Application.Features.Notification.Commands.AddNewNotification
{
    public class AddNotificationHandler : IRequestHandler<AddNotificationQuery, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public AddNotificationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<BaseResponse<string>> Handle(AddNotificationQuery request, CancellationToken cancellationToken)
        {
            var Not = new Notifications
            {
                Title= request.Title,
                Description = request.Description,
                CreatedAt= DateTime.UtcNow,

            };
            var message = new Message()
            {
                Topic = "AllUsers",
                Notification = new FirebaseAdmin.Messaging.Notification 
                {
                    Title = request.Title,
                    Body = request.Description
                }
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            await _unitOfWork.Repository<int, Notifications>().AddAsync(Not);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<string>(true, "Notification added successfully");
        }
    }
}
