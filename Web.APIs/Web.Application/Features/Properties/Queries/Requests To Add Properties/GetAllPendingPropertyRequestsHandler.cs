using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

namespace Web.Application.Features.Properties.Queries.Requests_To_Add_Properties
{
    public class GetAllPendingPropertyRequestsHandler : IRequestHandler<GetAllPendingPropertyRequestsQuery, BaseResponse<List<RequestsToAddPropertiesDto>>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public GetAllPendingPropertyRequestsHandler(AppDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public async Task<BaseResponse<List<RequestsToAddPropertiesDto>>> Handle(GetAllPendingPropertyRequestsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Properties
       .Where(p => p.PropertyType == request.PropertyType&& p.IsActive==false)
       .AsNoTracking()
       .OrderByDescending(p => p.Id)
       .AsQueryable();
            int totalCount = query.Count();
            int skip = (request.PageNumber - 1) * request.PageSize;

            var properties = await query
                .Skip(skip)
                .Take(request.PageSize)
               .Select(p => new RequestsToAddPropertiesDto
               {
                   Id = p.Id,
                   Governorate=p.Governorate,
                   PropertyType = request.PropertyType,
                   City = p.City,
                   UserFullName = p.Owner.FullName,
                   CreatedAt=p.CreatedAt,
                   UserImage = p.Owner.ProfileImage != null ? $"{_configuration["BaseURL"]}/User/{p.MainImage}" : null,
                   PropertyMainImage = p.MainImage != null ? $"{_configuration["BaseURL"]}/Property/{p.MainImage}" : null,
               })
.ToListAsync(cancellationToken);
            var totalPage = (int)Math.Ceiling(totalCount / (double)request.PageSize);
            return new BaseResponse<List<RequestsToAddPropertiesDto>>(true, "تم جلب العقارات بنجاح", properties,totalCount, request.PageNumber, request.PageSize, totalPage);
        }
    }
}
