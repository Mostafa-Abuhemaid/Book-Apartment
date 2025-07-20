
using MediatR;
using Web.Application.Response;
using Web.Domain.Repositories;
using Web.Domain.Entites;
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
            await _unitOfWork.Repository<int, Notifications>().AddAsync(Not);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<string>(true, "Notification added successfully!");
        }
    }
}
