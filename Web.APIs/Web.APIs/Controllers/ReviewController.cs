using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.ReviewDto;
using Web.Application.Features.Reviews.Commands.AddReview;
using Web.Application.Features.Reviews.Queries.GetReviews;

namespace Web.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ReviewController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ReviewController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("add-review")]
		public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
		{
			return Ok(await _mediator.Send(command));
		}

		[HttpGet("get-property-reviews/{id}")]
		public async Task<IActionResult> GetPropertyRevies(int id)
		{
			var command = new GetPropertyReviewsQuery(id);
			return Ok(await _mediator.Send(command));
		}
	}
}
