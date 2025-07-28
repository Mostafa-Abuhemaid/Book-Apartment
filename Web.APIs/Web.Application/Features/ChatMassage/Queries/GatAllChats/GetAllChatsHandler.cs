
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.ChatMassageDto;
using Web.Domain.Entites;
using Web.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.Application.Features.ChatMassage.Queries.GatAllChats
{
    public class GetAllChatsHandler : IRequestHandler<GetAllChatsCommand, BaseResponse<List<ChatDto>>>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public GetAllChatsHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<BaseResponse<List<ChatDto>>> Handle(GetAllChatsCommand request, CancellationToken cancellationToken)
        {
            var chats = await _context.Chats
                .AsNoTracking()
                .Where(u => u.FirstUserId == request.userId || u.SecondUserId == request.userId)
                .Include(m => m.Messages)
                .ToListAsync();

            var userIds = chats
                .Select(c => c.FirstUserId == request.userId ? c.SecondUserId : c.FirstUserId)
                .Distinct()
                .ToList();

            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id);

            var results = new List<ChatDto>();

            foreach (var chat in chats)
            {
                var LastMessage = chat.Messages
                    .OrderByDescending(m => m.CreatedAt)
                    .FirstOrDefault();

                var otherUserId = chat.FirstUserId == request.userId ? chat.SecondUserId : chat.FirstUserId;
                var user = users[otherUserId];

                var unreadCount = chat.Messages.Count(m => m.IsRead == false && m.ReceiverUserId == request.userId);

                var chatDto = new ChatDto
                {
                    ChatId = chat.Id,
                    SenderId = user.Id,
                    UserName = user.UserName!,
                    UserImage = user.ProfileImage != null
                        ? $"{_configuration["BaseURL"]}/User/{user.ProfileImage}"
                        : null,
                    UnReaded = unreadCount,
                    LastMessage = LastMessage?.Content,
                    Date = LastMessage?.CreatedAt ?? chat.CreatedAt
            };

                results.Add(chatDto);
            }

            return new BaseResponse<List<ChatDto>>(true, "تم الوصول الى جميع المحادثات بنجاح", results);
        }

    }
}
