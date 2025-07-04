using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Properties.Queries.Requests_To_Add_Properties;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Queries.Get_Property_By_Id
{
    public class GetPropertyByIdRequestsHandler : IRequestHandler<GetPropertyByIdRequestsQuery, BaseResponse<GetPropertyDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public GetPropertyByIdRequestsHandler(AppDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

    

        public async Task<BaseResponse<GetPropertyDto>> Handle(GetPropertyByIdRequestsQuery request, CancellationToken cancellationToken)
        {
           var property = await _dbContext.Properties.
                Include(p=>p.Images)
                .Include(p=>p.Owner)
                .FirstOrDefaultAsync(x => x.Id == request.PropertyId);
            if (property == null)
                return new BaseResponse<GetPropertyDto>(false, "هذه الوحدة غير موجودة");
            var dto = property.Adapt<GetPropertyDto>();
        
            
            return new BaseResponse<GetPropertyDto>(true, "تم الوصول الي الوحدة العقارية ",dto);
        }
    }
}