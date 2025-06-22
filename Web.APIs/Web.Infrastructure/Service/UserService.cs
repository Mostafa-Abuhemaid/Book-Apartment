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

        public async Task<BaseResponse<List<UserDto>>> GetAllUsersAsync(int pageNumber = 10, int pageSize = 10)
        {
           
            int skip = (pageNumber - 1) * pageSize;

           
            var users = await _userManager.Users
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,

                    Role = roles.FirstOrDefault()
                };

                userDtos.Add(userDto);
            }

            return new BaseResponse<List<UserDto>>(true, "Reached Users successfully", userDtos);
        }


        public async Task<BaseResponse<UserDto>> GetUserDetailsAsync(string userId)
        {
            //  var user = await _userManager.FindByIdAsync(userId);
            //  if (user == null) return new BaseResponse<UserDto>(false, $"No User with this Id : {userId}");
            //  var userDTO = _mapper.Map<UserDto>(user);
            //
            //  return new BaseResponse<UserDto>(true, $"Reached User with Id : {userId}", userDTO);
            return null;
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
