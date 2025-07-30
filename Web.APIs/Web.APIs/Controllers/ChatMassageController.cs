using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Application.Features.ChatMassage.Commands.AddNewMassage;
using Web.Application.Features.ChatMassage.Commands.DeleteMassage;
using Web.Application.Features.ChatMassage.Queries.GatAllChats;
using Web.Application.Features.ChatMassage.Queries.GetAllMassage;
using Web.Application.Features.Notification.Commands.AddNewNotification;
using Web.Application.Response;
using Web.Domain.DTOs.ChatMassageDto;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMassageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatMassageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private string GetUserId()
        {
         
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        [Authorize]
        [HttpPost]   
        public async Task<IActionResult> AddNewMassage([FromBody] SendMassageDto dto)
        {
            var ReceiverUserId = GetUserId();
            if (string.IsNullOrEmpty(ReceiverUserId))
                return Unauthorized("المستخدم غير مصدق");
            var massage = new AddNewMassageCommand(ReceiverUserId, dto.ReceiverUserId, dto.Content,dto.ChatId);
            return Ok(await _mediator.Send(massage));
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
        public async Task<IActionResult> AddNewMassage(string otherUserId)
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
    }
}
