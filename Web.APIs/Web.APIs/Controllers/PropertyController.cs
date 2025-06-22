using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.PropertyDTO;
using Web.Application.Features.Properties.Commands.AddNewProperty;
using Web.Application.Features.Properties.Commands.AddPropertyToFavorit;
using Web.Application.Features.Properties.Queries.GetFavorit;
using Web.Application.Features.Properties.Queries.Requests_To_Add_Properties;

namespace Web.APIs.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	
	public class PropertyController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PropertyController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
        [HttpPost()]
        [DisableRequestSizeLimit] // لو الصور حجمها كبير
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("AddOrDeleteFavorit/{id}")]

		[Authorize]
		public async Task<IActionResult> FlagPropertyAsFavorit(int id)
		{
			var command = new AddPropertyToFavoritCommand(id);
			return Ok(await _mediator.Send(command));
		}
        [Authorize]
        [HttpGet("GetFavoritProperties")]
		public async Task<IActionResult> GetFavoritProperties()
		{
			var query = new GetFavoritQuery();
			return Ok(await _mediator.Send(query));
		}
        [HttpGet("GetRequestsToAddProperties")]
        public async Task<IActionResult> GetRequestsToAddProperties()
        {
            var query = new GetAllPendingPropertyRequestsQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
