using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Lock/{UserId}")]
        public async Task<IActionResult> LockUser(string UserId)
        {
            var user = await _userService.LockUserByEmailAsync(UserId);
            return user.Success ? Ok(user) : BadRequest(user);
        }


        //  [Authorize(Roles = "Admin")]
        [HttpPost("Unlock/{UserId}")]
        public async Task<IActionResult> UnlockUser(string UserId)
        {
            var user = await _userService.UnlockUserByEmailAsync(UserId);
            return user.Success ? Ok(user) : BadRequest(user);
        }
        // [Authorize(Roles = "Admin")] 
        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUserByEmail(string UserId)
        {
            var user = await _userService.DeleteUserByIdAsync(UserId);
            return user.Success ? Ok(user) : BadRequest(user);
        }
        // [Authorize(Roles = "Admin")] 
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int pageNumber, int pageSize)
        {
            var user = await _userService.GetAllUsersAsync( pageNumber,  pageSize);
            return user.Success ? Ok(user) : BadRequest(user);
        }
        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var user = await _userService.GetAdminDetailsAsync();
            return user.Success ? Ok(user) : BadRequest(user);
        }
        [HttpGet("SearshByUserName")]
        public async Task<IActionResult> SearshForUsersAsync(string Name)
        {
            var user = await _userService.SearshForUsersAsync(Name);
            return user.Success ? Ok(user) : BadRequest(user);
        }
    }
}
