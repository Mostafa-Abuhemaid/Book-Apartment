using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Queries.Get_All_Property
{
    public class GetPropertiesByTypeQuery : IRequest<BaseResponse<List<GetAllPropertiesDto>>>
    {
        public PropertyType PropertyType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public RentType? RentType { get; set; } // طلابي ولا اسر 
        public bool? IsFurnished { get; set; } = false; // مفروشة ولا لاء
        public PropertyNature? Type { get; set; } // شقة، فيلا، شاليه، سطح
    }
}
