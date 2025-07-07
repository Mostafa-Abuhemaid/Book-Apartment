using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.UserDTO;
using Web.Application.Response;

namespace Web.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<List<UserDto>>> GetAdminDetailsAsync();
        Task<BaseResponse<List<UserDto>>> GetAllUsersAsync(int pageNumber , int pageSize );
        Task<BaseResponse<List<UserDto>>> SearshForUsersAsync(string UserName);
        Task<BaseResponse<bool>> EditUserAsync( UserDto model);

        Task<BaseResponse<bool>> LockUserByEmailAsync(string UserId);
        Task<BaseResponse<bool>> UnlockUserByEmailAsync(string UserId);
        Task<BaseResponse<bool>> DeleteUserByIdAsync(string userId);

    }
}
