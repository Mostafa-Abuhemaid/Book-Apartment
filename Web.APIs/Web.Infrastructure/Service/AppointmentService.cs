using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public AppointmentService(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<BaseResponse<bool>> AddNewAppointmentAsync(CreateAppointmentDto dto)
        {
           var property = await _dbContext.Properties.FirstOrDefaultAsync(p => p.Id == dto.PropertyId);   
            if(property == null)           
                return new BaseResponse<bool>(false, "هذا العقار غير موجود ");
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return new BaseResponse<bool>(false, $"هذا المستخدم غير موجود");
            var existing = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.PropertyId == dto.PropertyId && a.UserId == dto.UserId);
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

    }
}
