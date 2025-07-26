using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Infrastructure.Data;

namespace Web.Application.Features.ChatMassage.Queries.GetAllMassage
{
    public class GetChatHistoryHandler : IRequestHandler<GetChatHistoryCommand, BaseResponse<List<ChatMessage>>>
    {
        private readonly AppDbContext _context;
        public GetChatHistoryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task <BaseResponse<List<ChatMessage>>> Handle(GetChatHistoryCommand request, CancellationToken cancellationToken)
        {
            var messages = await _context.ChatMessages
      .Where(m => (m.SenderUserId ==request. userId && m.ReceiverUserId ==request. otherUserId) ||
                  (m.SenderUserId == request.otherUserId && m.ReceiverUserId == request.userId))
      .OrderByDescending(m => m.CreatedAt)
      .Take(50) 
      .ToListAsync();
            return new BaseResponse<List<ChatMessage>>(true, "تم الوصول للمحادثات", messages);
        }
    }
}
