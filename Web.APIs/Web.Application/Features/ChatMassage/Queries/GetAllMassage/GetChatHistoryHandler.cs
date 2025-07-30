using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.ChatMassageDto;
using Web.Domain.Entites;
using Web.Infrastructure.Data;

namespace Web.Application.Features.ChatMassage.Queries.GetAllMassage
{
    public class GetChatHistoryHandler : IRequestHandler<GetChatHistoryCommand, BaseResponse<List<GetChatDto>>>
    {
        private readonly AppDbContext _context;
        public GetChatHistoryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task <BaseResponse<List<GetChatDto>>> Handle(GetChatHistoryCommand request, CancellationToken cancellationToken)
        {
            var chat = await _context.Chats
     .Include(c => c.Messages)
     .FirstOrDefaultAsync(m =>
         (m.FirstUserId == request.userId && m.SecondUserId == request.otherUserId) ||
         (m.SecondUserId == request.userId && m.FirstUserId == request.otherUserId), cancellationToken);

            if (chat == null)
                return new BaseResponse<List<GetChatDto>>(true, "لا يوجد محادثة", null);

            var message = chat.Messages
                .OrderBy(m => m.CreatedAt)
                .Select(m => new GetChatDto
                {
                    Id = m.Id,
                    ChatId = m.ChatId,
                    Content = m.Content,
                    IsRead = m.IsRead,
                    SenderUserId = m.SenderUserId,
                    CreatedAt = m.CreatedAt,
                    ReceiverUserId = m.ReceiverUserId
                })
                .ToList();

            return new BaseResponse<List<GetChatDto>>(true, "تم الوصول للمحادثات", message);

        }
    }
}
