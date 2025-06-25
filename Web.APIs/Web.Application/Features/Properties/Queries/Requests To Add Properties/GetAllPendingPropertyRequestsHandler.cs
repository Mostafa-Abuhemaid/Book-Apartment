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
            var requests = await _dbContext.Properties
                .Where(p => p.IsActive==false)
                .Include(p => p.Owner)
                .Select(p => new RequestsToAddPropertiesDto
                {
                    Id = p.Id,
                    City = p.City,                  
                    UserFullName = p.Owner.FullName,
                    UserImage = p.Owner.ProfileImage != null ? $"{_configuration["BaseURL"]}/User/{p.MainImage}" : null,
                    PropertyMainImage = p.MainImage != null ? $"{_configuration["BaseURL"]}/Property/{p.MainImage}" : null,
                })
                .ToListAsync(cancellationToken);

            return new BaseResponse<List<RequestsToAddPropertiesDto>>(true,"تم جلب الطلبات بنجاح",requests);
        }
    }
}
