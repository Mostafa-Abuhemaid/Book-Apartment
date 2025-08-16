using Azure.Core;
using FirebaseAdmin.Messaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Web.Application.Features.ChatMassage.Commands.AddNewMassage;
using Web.Application.Features.ChatMassage.Commands.DeleteMassage;
using Web.Application.Features.ChatMassage.Commands.ReadChatMessages;
using Web.Application.Features.ChatMassage.Queries.GatAllChats;
using Web.Application.Features.ChatMassage.Queries.GetAllMassage;
using Web.Application.Features.Notification.Commands.AddNewNotification;
using Web.Application.Hubs;
using Web.Application.Response;
using Web.Domain.DTOs.ChatMassageDto;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMassageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatMassageController(IMediator mediator, IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }
        private string GetUserId()
        {
         
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        [Authorize]
        [HttpPost]   
        public async Task<IActionResult> AddNewMassage([FromBody] SendMassageDto dto)
        {
            var SenderUserId = GetUserId();
            if (string.IsNullOrEmpty(SenderUserId))
                return Unauthorized("المستخدم غير مصدق");
            var massage = new AddNewMassageCommand(SenderUserId, dto.ReceiverUserId, dto.Content,dto.ChatId);
            var res = await _mediator.Send(massage);
            var mass = res.Data;
            await _hubContext.Clients.User(SenderUserId).SendAsync("MessageSent", mass);
            await _hubContext.Clients.User(dto.ReceiverUserId).SendAsync("ReceiveMessage", mass);
            return Ok(res);

        }
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMassage(int Id)
        {
            var ReceiverUserId = GetUserId();
            if (string.IsNullOrEmpty(ReceiverUserId))
                return Unauthorized("المستخدم غير مصدق");
            var massage = new DeleteMassageCommand(Id);
            return Ok(await _mediator.Send(massage));
        }
        [Authorize]
        [HttpGet("history/{otherUserId}")]
        public async Task<IActionResult> GetChatMassages(string otherUserId)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("المستخدم غير مصدق.");
            var massage = new GetChatHistoryCommand(userId,otherUserId);
            return Ok(await _mediator.Send(massage));
        }
        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new GetAllChatsCommand ( userId );

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPut("MakeAllMassageRead")]
        public async Task<IActionResult> MakeAllMassageRead(int ChatId)
        {
          
            var command = new ReadChatMessagesCommand(ChatId);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
