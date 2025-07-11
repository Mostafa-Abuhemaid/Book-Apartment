using Mapster.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Queries.Property_Dashboard
{
    public class PropertyDashboardQuery : IRequest<BaseResponse<List<PropertyDashboardDto>>>
    {
        public PropertyType PropertyType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PropertySaleStatus? PropertySaleStatus { get; set; } 
        public PropertyRentStatus? PropertyRentStatus { get; set; }
    }
}
