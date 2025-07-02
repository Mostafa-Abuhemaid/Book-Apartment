using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.PropertyDTO;
using Web.Application.Features.Properties.Commands.AddNewProperty;
using Web.Application.Features.Properties.Commands.AddPropertyToFavorit;
using Web.Application.Features.Properties.Commands.ApproveProperty;
using Web.Application.Features.Properties.Commands.DeleteProperty;
using Web.Application.Features.Properties.Queries.Filter_Properties;
using Web.Application.Features.Properties.Queries.Get_All_Property;
using Web.Application.Features.Properties.Queries.Get_Property_By_Id;
using Web.Application.Features.Properties.Queries.GetFavorit;
using Web.Application.Features.Properties.Queries.Requests_To_Add_Properties;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Enums;

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
        public async Task<IActionResult> GetRequestsToAddProperties(PropertyType PropertyType, int PageSize, int PageNumber)
        {
            var query = new GetAllPendingPropertyRequestsQuery(PropertyType, PageNumber, PageSize);
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPropertyById(int Id)
        {
            var query = new GetPropertyByIdRequestsQuery(Id);
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("Filter")]
        public async Task<IActionResult> FilterProperties([FromBody] FilterPropertiesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetByType")]
        public async Task<IActionResult> GetByType([FromQuery] GetPropertiesByTypeQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property= new DeletePropertyCommand (id);
           
            return Ok(await _mediator.Send(property));
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> AcceptProperty(int id)
        {
            var property = new ApprovePropertyCommand(id);
            var result = await _mediator.Send(property);
            return Ok(result);
        }

    }
}
