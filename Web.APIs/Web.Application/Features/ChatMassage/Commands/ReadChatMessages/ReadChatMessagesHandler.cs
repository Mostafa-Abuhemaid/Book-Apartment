using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.ChatMassage.Commands.AddNewMassage;
using Web.Application.Response;
using Web.Infrastructure.Data;

namespace Web.Application.Features.ChatMassage.Commands.ReadChatMessages
{
    public class ReadChatMessagesHandler : IRequestHandler<ReadChatMessagesCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;
        public ReadChatMessagesHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<bool>> Handle(ReadChatMessagesCommand request, CancellationToken cancellationToken)
        {
            var chatExists = await _context.Chats.AnyAsync(c => c.Id == request.ChatId, cancellationToken);
            if (!chatExists)
                return new BaseResponse<bool>(false, "لا يوجد محادثة");

            var unReadMessages = await _context.ChatMessages
                .Where(m => m.ChatId == request.ChatId && !m.IsRead)
                .ToListAsync(cancellationToken);

            foreach (var message in unReadMessages)
                message.IsRead = true;

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<bool>(true, "All messages marked as read");

        }
    }
}
