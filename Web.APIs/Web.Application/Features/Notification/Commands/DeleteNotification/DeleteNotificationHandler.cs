using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Reviews.Commands.DeleteReview;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.Notification.Commands.DeleteNotification
{
    internal class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, BaseResponse<bool>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public DeleteNotificationHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                return new BaseResponse<bool>(false, "Invalid Id");
            }
            var Not = await _unitOfWork.Repository<int, Notifications>().GetByIdAsync(request.Id);
            if (Not == null)
            {
                return new BaseResponse<bool>(false, "Notification not found!");
            }

            _unitOfWork.Repository<int, Notifications>().Remove(Not);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Your Notification removed successfully ");
        }
    }

}
