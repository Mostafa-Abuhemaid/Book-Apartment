using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Hubs;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;
using Web.Infrastructure.Data;

namespace Web.Application.Features.ChatMassage.Commands.AddNewMassage
{
    public class AddNewMassageHandler : IRequestHandler<AddNewMassageCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;

        private readonly IHubContext<ChatHub> _hubContext;
        public AddNewMassageHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext, AppDbContext context)
        {
            

            _hubContext = hubContext;
            _context = context;
        }
        public async Task<BaseResponse<bool>> Handle(AddNewMassageCommand request, CancellationToken cancellationToken)
        {
            Chat? chat = null;

            if (request.ChatId == 0 || request.ChatId == null)
            {
                chat = await _context.Chats
                  .FirstOrDefaultAsync(c =>
                  (c.FirstUserId == request.SenderUserId && c.SecondUserId == request.ReceiverUserId) ||
                  (c.SecondUserId == request.SenderUserId && c.FirstUserId == request.ReceiverUserId));

                if (chat == null)
                {
                    chat = new Chat
                    {
                        FirstUserId = request.SenderUserId,
                        SecondUserId = request.ReceiverUserId
                    };
                    await _context.Chats.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                chat = await _context.Chats.FindAsync(request.ChatId);
                if (chat == null)
                    return new BaseResponse<bool>(false, "Chat does not exist");
            }

            var message = new ChatMessage
            {
                SenderUserId = request.SenderUserId,
                ReceiverUserId = request.ReceiverUserId,
                Content = request.Content,
                ChatId = chat.Id
            };

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();

            // تأكد من تحميل الرسالة كاملة لو هتبعتها فيها معلومات أكثر
            await _context.Entry(message).ReloadAsync();

            await _hubContext.Clients.User(request.SenderUserId).SendAsync("MessageSent", message);
            await _hubContext.Clients.User(request.ReceiverUserId).SendAsync("ReceiveMessage", message);

            return new BaseResponse<bool>(true, "Message sent");
        }

    }
}
