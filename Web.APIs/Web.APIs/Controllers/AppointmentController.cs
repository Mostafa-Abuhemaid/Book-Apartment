using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Response;
using Web.Domain.DTOs.AccountDTO;
using Web.Domain.DTOs.AppointmentDto;
using Web.Domain.Interfaces;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpPost()]
        public async Task<IActionResult> AddNewAppointmentAsync([FromBody]CreateAppointmentDto dto)
        {
            var result = await _appointmentService.AddNewAppointmentAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpGet()]
        public async Task<IActionResult> GetAllAppointments(int PageNumber, int PageSize)
        {
            var result = await _appointmentService.GetAllAppointmentAsync( PageNumber,  PageSize);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
