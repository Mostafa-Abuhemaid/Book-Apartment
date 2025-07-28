using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.ChatMassageDto;
using Web.Domain.Entites;

namespace Web.Application.Features.ChatMassage.Queries.GatAllChats
{
    public record GetAllChatsCommand(string userId) : IRequest<BaseResponse<List<ChatDto>>>;

}
