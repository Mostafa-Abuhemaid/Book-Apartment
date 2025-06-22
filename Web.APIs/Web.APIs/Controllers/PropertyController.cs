using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.PropertyDTO;
using Web.Application.Features.Properties.Commands.AddNewProperty;
using Web.Application.Features.Properties.Commands.AddPropertyToFavorit;
using Web.Application.Features.Properties.Queries.GetFavorit;

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
		
        [HttpPost("create-property")]
        [DisableRequestSizeLimit] // لو الصور حجمها كبير
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("favorit-flag/{id}")]
		public async Task<IActionResult> FlagPropertyAsFavorit(int id)
		{
			var command = new AddPropertyToFavoritCommand(id);
			return Ok(await _mediator.Send(command));
		}

		[HttpGet("get-favorit-properties/{userId}")]
		public async Task<IActionResult> GetFavoritProperties(string userId)
		{
			var query = new GetFavoritQuery(userId);
			return Ok(await _mediator.Send(query));
		}
	}
}
