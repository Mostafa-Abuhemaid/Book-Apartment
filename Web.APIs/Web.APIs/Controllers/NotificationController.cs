using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Features.Notification.Commands.AddNewNotification;
using Web.Application.Features.Notification.Commands.DeleteNotification;
using Web.Application.Features.Notification.Queries.Get_All_Notification;
using Web.Application.Features.Properties.Commands.AddNewProperty;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewNorification([FromBody] AddNotificationQuery command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNorification([FromQuery] GetAllNotificationQuery command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteNorification([FromRoute]int Id)
        {
          var command=new  DeleteNotificationCommand(Id);
            return Ok(await _mediator.Send(command));
        }
    }
}
