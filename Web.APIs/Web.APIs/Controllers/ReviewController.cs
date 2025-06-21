using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.ReviewDto;
using Web.Application.Features.Reviews.Commands.AddReview;
using Web.Application.Features.Reviews.Commands.DeleteReview;
using Web.Application.Features.Reviews.Commands.UpdateReview;
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

		[HttpPost()]
		public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
		{
			return Ok(await _mediator.Send(command));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPropertyRevies(int id)
		{
			var command = new GetPropertyReviewsQuery(id);
			return Ok(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveReview(int id)
		{
			var command = new DeleteReviewCommand(id);
			return Ok(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateReview(int id, int Stars,string Comment)
		{
			var command = new UpdateReviewCommand(id, Comment, Stars);
			return Ok(await _mediator.Send(command));
		}

	}
}
