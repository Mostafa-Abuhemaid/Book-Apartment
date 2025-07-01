using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.Dashboard;
using Web.Domain.Entites;
using Web.Domain.Enums;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Dashboard.Queries.DashboardStats
{
    public class DashboardStatsQueryHandler : IRequestHandler<DashboardStatsQuery, BaseResponse<DashboardStatsDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public DashboardStatsQueryHandler(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task< BaseResponse< DashboardStatsDto>> Handle(DashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var total = await _dbContext.Properties.AsNoTracking().CountAsync();
            var rented = await _dbContext.Properties.AsNoTracking().CountAsync(p => p.PropertyType== PropertyType.Rent);
        
            var sold = await _dbContext.Properties.AsNoTracking().CountAsync(p => p.PropertyType == PropertyType.Sale);

            var totalUsers = await _userManager.Users.AsNoTracking().CountAsync();
            var blockedUsers = await _userManager.Users.AsNoTracking().CountAsync(u => u.LockoutEnd != null && u.LockoutEnd > DateTimeOffset.UtcNow);

            var dto= new DashboardStatsDto
            {
                TotalUnits = total ,
                RentedUnits = rented,             
                SoldUnits = sold,
                TotalUsers =totalUsers,            
                BlockedUsers = blockedUsers
               
            };
            return new BaseResponse<DashboardStatsDto>(true, "تم الوصول الي البيانات بنجاح ", dto);
        }
    }
}
