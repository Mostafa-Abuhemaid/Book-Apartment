using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Entites;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Queries.Filter_Properties
{
    public class FilterPropertiesHandler : IRequestHandler<FilterPropertiesQuery, BaseResponse<List<GetAllPropertiesDto>>>
    {
        private readonly AppDbContext _dbContext;

        private readonly IConfiguration _configuration;
        public FilterPropertiesHandler(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<BaseResponse<List<GetAllPropertiesDto>>> Handle(FilterPropertiesQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Properties
            .Include(p => p.Images)
            .AsNoTracking()
            .Where(p => p.IsActive==false);

            if (request.Type!= null)
                query = query.Where(p => p.RentType== request.Type);

            if (request.PropertyNature != null)
                query = query.Where(p => p.Type == request.PropertyNature);

            if (request.AvailabilityStatus != null)
                query = query.Where(p => p.AvailabilityStatus == request.AvailabilityStatus);
          
            if (request.Rooms!=null)
                query = query.Where(p => p.Rooms == request.Rooms);

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice.Value);

            if (!string.IsNullOrEmpty(request.Floor))
            {
                query = query.Where(p => p.Floor == request.Floor ||
                    (request.Floor == "الارضي" && (p.Floor == "0" || p.Floor == "أرضي")));
            }

            if (request.HasWifi.HasValue)
                query = query.Where(p => p.HasWifi == request.HasWifi);

            var propertyDtos = query.Select(p => new GetAllPropertiesDto
            {
                Id = p.Id,
                Governorate = p.Governorate,
                City = p.City,
                Rooms = p.Rooms,
                Area = p.Area,
                Price = p.Price,
                Floor = p.Floor,
                MainImage = !string.IsNullOrEmpty(p.MainImage)
           ? $"{_configuration["BaseURL"]}/User/{p.MainImage}"
           : null
            }).ToList();

            return new BaseResponse<List<GetAllPropertiesDto>>(true, "نتائج البحث ", propertyDtos);
        }
    }
}
