using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Hubs;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.ChatMassage.Commands.AddNewMassage
{
    public class AddNewMassageHandler : IRequestHandler<AddNewMassageCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
     
        private readonly IHubContext<ChatHub> _hubContext;
        public AddNewMassageHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
           
            _hubContext = hubContext;
        }
        public async Task<BaseResponse<bool>> Handle(AddNewMassageCommand request, CancellationToken cancellationToken)
        {
            var message = new ChatMessage
            {
                SenderUserId =  request.SenderUserId,
                ReceiverUserId =request.ReceiverUserId,
                Content = request.Content
            };

            _unitOfWork.Repository<int,ChatMessage>().AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            await _hubContext.Clients.User(request.SenderUserId).SendAsync("MessageSent", message);
            await _hubContext.Clients.User(request.ReceiverUserId).SendAsync("ReceiveMessage", message);
            return new BaseResponse<bool>(true, "Message sent!");
        }
    }
}
