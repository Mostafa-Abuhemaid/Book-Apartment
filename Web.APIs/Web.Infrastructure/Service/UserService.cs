using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.UserDTO;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Entites;


namespace Web.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
       
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
           
        }
        public async Task<BaseResponse<bool>> DeleteUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse<bool>(false, $"No user found with ID: {userId}");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return new BaseResponse<bool>(false, "Failed to delete user");

            return new BaseResponse<bool>(true, $"User with Name {user.FullName} deleted successfully");
        }


        public async Task<BaseResponse<bool>> EditUserAsync([FromBody] UserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) new BaseResponse<bool>(false, $"No User with this email : {model.Id}");


            user.UserName = model.UserName;
            user.Email = model.Email;


            var result = await _userManager.UpdateAsync(user);
            return new BaseResponse<bool>(true, $"User {model.UserName} Updated successfully");
        }

        public async Task<BaseResponse<List<UserDto>>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _userManager.Users.CountAsync();
         

            var users = await _userManager.Users
                .OrderBy(u => u.Id)
                 .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsBlocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow
                })
        .ToListAsync();

          


            return new BaseResponse<List<UserDto>>(true, "Reached Users successfully", users, totalCount,pageNumber,pageSize);
        }


        public async Task<BaseResponse<List<UserDto>>> GetAdminDetailsAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");

            var adminDtos = adminUsers.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
            }).ToList();

            return new BaseResponse<List<UserDto>>(true, "تم الوصول الي البيانات بنجاح", adminDtos);
        }


        public async Task<BaseResponse<bool>> LockUserByEmailAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null) new BaseResponse<bool>(false, $"No User with this Id ");

            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            return new BaseResponse<bool>(true, $"User {user.UserName} locked successfully");
        }

        public async Task<BaseResponse<bool>> UnlockUserByEmailAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null) return new BaseResponse<bool>(false, $"No User with this Id");

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            return new BaseResponse<bool>(true, $"User {user.UserName} unlocked successfully");
        }
    }
}
