using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.AppointmentDto;

namespace Web.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<BaseResponse<bool>> AddNewAppointmentAsync(CreateAppointmentDto dto);
        Task<BaseResponse<List<GetAppointmentDto>>> GetAllAppointmentAsync(int PageNumber, int PageSize);
        Task<BaseResponse<GetAppointmentDto>> GetByIdAppointmentAsync(int id);
        Task<BaseResponse<bool>> DeleteAppointmentAsync(int id);
        Task<BaseResponse<bool>> EditAppointmentAsync(int id,string Notes);

    }
}
