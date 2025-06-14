using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Features.Properties.Commands.AddPropertyToFavorit;

namespace Web.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PropertyController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PropertyController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("favorit-flag/{id}")]
		public async Task<IActionResult> FlagPropertyAsFavorit(int id)
		{
			var command = new AddPropertyToFavoritCommand(id);
			return Ok(await _mediator.Send(command));
		}

	}
}
