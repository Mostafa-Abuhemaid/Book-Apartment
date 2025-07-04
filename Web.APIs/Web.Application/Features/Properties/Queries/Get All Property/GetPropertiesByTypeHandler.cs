using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Queries.Get_All_Property
{
    public class GetPropertiesByTypeHandler : IRequestHandler<GetPropertiesByTypeQuery, BaseResponse<List<GetAllPropertiesDto>>>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public GetPropertiesByTypeHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<BaseResponse<List<GetAllPropertiesDto>>> Handle(GetPropertiesByTypeQuery request, CancellationToken cancellationToken)
        {
            var query =  _context.Properties
                .Where( p=>p.PropertyType == request.PropertyType)
                .AsNoTracking()
                .OrderByDescending(p => p.Id) 
                .AsQueryable();

            if (request.IsFurnished != null)
                query = query.Where(p => p.IsFurnished == request.IsFurnished);
            if (request.RentType != null)
                query = query.Where(p => p.RentType == request.RentType);

            if (request.Type != null)
                query = query.Where(p => p.Type == request.Type);


            int skip = (request.PageNumber - 1) * request.PageSize;

            var properties = await query
                .Skip(skip)
                .Take(request.PageSize)
                .Select(p => new GetAllPropertiesDto
                {
                    Id = p.Id,
                    Governorate = p.Governorate,
                    City = p.City,
                    Rooms = p.Rooms,
                    Area = p.Area,
                    Price = p.Price,
                    Floor = p.Floor,
                    MainImage = !string.IsNullOrEmpty(p.MainImage)
           ? $"{_configuration["BaseURL"]}/Property/{p.MainImage}"
           : null,
                    Type = p.Type,
                    CreatedAt = p.CreatedAt,
                   
                })
                .ToListAsync(cancellationToken);
            int totalCount = query.Count();
            var totalPage = (int)Math.Ceiling(totalCount / (double)request.PageSize);
            return new BaseResponse<List<GetAllPropertiesDto>>(true, "تم جلب العقارات بنجاح", properties, totalCount, request.PageNumber, request.PageSize, totalPage);
        }


    }
}
