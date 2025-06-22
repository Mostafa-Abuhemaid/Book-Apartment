using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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

        public Task<BaseResponse<bool>> DeleteAppointmentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<GetAppointmentDto>>> GetAllAppointmentAsync()
        {
            var appointments = await _dbContext.Appointments
         .Include(a => a.Property)
             .ThenInclude(p => p.Owner)
         .ToListAsync();

            var result = appointments.Select(a => new GetAppointmentDto
            {
                Id = a.Id,
                PropertyId = a.Property.Id,
                Tilte = a.Property.Title,
                OwnerName = a.Property.Owner.FullName,
                OwnerPhone = a.Property.Owner.PhoneNumber,
                MainImage = a.Property.MainImage != null ? $"{_configuration["BaseURL"]}/User/{a.Property.MainImage}" : null,
                PropertyType = a.Property.PropertyType,
                Address = a.Property.Address,
                OwnerId = a.Property.OwnerId
            }).ToList();

            return new BaseResponse<List<GetAppointmentDto>>(true, "تم جلب حميع المقابلات  بنجاح",result);
        }
    }
}
