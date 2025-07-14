using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.AppointmentDto;
using Web.Domain.Entites;
using Web.Domain.Interfaces;
using Web.Infrastructure.Data;

namespace Web.Infrastructure.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public AppointmentService(AppDbContext dbContext, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<BaseResponse<bool>> AddNewAppointmentAsync(CreateAppointmentDto dto)
        {
           var property = await _dbContext.Properties.FirstOrDefaultAsync(p => p.Id == dto.PropertyId);   
            if(property == null)           
                return new BaseResponse<bool>(false, "هذا العقار غير موجود ");
            var user = await _userManager.FindByIdAsync(dto.RequesterId);
            if (user == null)
                return new BaseResponse<bool>(false, $"هذا المستخدم غير موجود");
            var existing = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.PropertyId == dto.PropertyId && a.UserId == dto.RequesterId);
            if (existing != null)
                return new BaseResponse<bool>(false, "لقد قمت بطلب معاينة لهذا العقار من قبل.");

            var app = new Appointment
            {
                PropertyId = dto.PropertyId,
                UserId = user.Id

            };
            await _dbContext.Appointments.AddAsync(app);
            await _dbContext.SaveChangesAsync();
            return new BaseResponse<bool>(true, "تم تقديم الطلب بنجاح ");
        }

        public async Task<BaseResponse<bool>> DeleteAppointmentAsync(int id)
        {
            var query = await _dbContext.Appointments.FirstOrDefaultAsync(x=>x.Id==id);
            if (query == null)
                return new BaseResponse<bool>(false, "لا يوجد طلب معاينة بهذا الرقم ");
            _dbContext.Appointments.Remove(query);
            await _dbContext.SaveChangesAsync();
            return new BaseResponse<bool>(true, "تم حذف طلب المعانية ");
        }

        public async Task<BaseResponse<List<GetAppointmentDto>>> GetAllAppointmentAsync(int PageNumber, int PageSize)
        {
            var query = _dbContext.Appointments
        .Select(a => new GetAppointmentDto
        {
            Id = a.Id,
            PropertyId = a.Property.Id,
            Tilte = a.Property.Title,
            OwnerName = a.Property.Owner.FullName,
            OwnerPhone = a.Property.Owner.PhoneNumber,
            OwnerImage = a.Property.Owner.ProfileImage != null
                ? $"{_configuration["BaseURL"]}/User/{a.Property.Owner.ProfileImage}"
                : null,           
            PropertyType = a.Property.PropertyType,
            RequesterId = a.UserId,
            RequesterName=a.User.FullName,
            RequesterPhone=a.User.PhoneNumber,
            RequesterImage= a.User.ProfileImage != null
                ? $"{_configuration["BaseURL"]}/User/{a.User.ProfileImage}"
                : null,
            CreatedAt = a.CreatedAt,
            Notes = a.Notes
          
        });

            int totalCount = await query.CountAsync();

            var pagedData = await query
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
                .ToListAsync();
            var totalPage = (int)Math.Ceiling(totalCount / (double)PageSize);
            return new BaseResponse<List<GetAppointmentDto>>(true, "تم جلب حميع المقابلات  بنجاح",pagedData ,totalCount,PageNumber,PageSize, totalPage);
        }
    }
}
