using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Properties.Queries.GetFavorit;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Enums;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Queries.Property_Dashboard
{
    public class PropertyDashboardHandler : IRequestHandler<PropertyDashboardQuery, BaseResponse<List<PropertyDashboardDto>>>
    {
        private readonly AppDbContext _context;
       

        public PropertyDashboardHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
           
        }

        public async Task<BaseResponse<List<PropertyDashboardDto>>> Handle(PropertyDashboardQuery request, CancellationToken cancellationToken)
        {
            var query =  _context.Properties
               .Where(p => p.PropertyType == request.PropertyType)
               .AsNoTracking()
               .OrderByDescending(p => p.Id)
               .AsQueryable();

         
            if (request.PropertySaleStatus != null)
                query = query.Where(p => p.PropertySaleStatus == request.PropertySaleStatus);
            if (request.PropertyRentStatus != null)
                query = query.Where(p => p.PropertyRentStatus == request.PropertyRentStatus);

            int skip = (request.PageNumber - 1) * request.PageSize;

            var properties = await query
                .Skip(skip)
                .Take(request.PageSize)
                .Select(p => new PropertyDashboardDto
                {
                    Id = p.Id,
                    Governorate = p.Governorate,
                    City = p.City,
                  OwnerName=p.Owner.FullName,
                    PropertyRentStatus=p.PropertyRentStatus,
                    PropertySaleStatus=p.PropertySaleStatus,
                    CreatedAt = p.CreatedAt,

                })
                .ToListAsync(cancellationToken);
            int totalCount = query.Count();
            var totalPage = (int)Math.Ceiling(totalCount / (double)request.PageSize);
            return new BaseResponse<List<PropertyDashboardDto>>(true, "تم جلب العقارات بنجاح", properties, totalCount, request.PageNumber, request.PageSize, totalPage);

        }
    }
}
