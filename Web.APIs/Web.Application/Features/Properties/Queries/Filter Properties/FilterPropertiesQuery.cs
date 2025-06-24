using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Queries.Filter_Properties
{
    public class FilterPropertiesQuery : IRequest<BaseResponse< List<GetAllPropertiesDto>>>
    {
        public string? Governorate { get; set; } // المحافظة 
        public string? City { get; set; } // المدينة 
        public PropertyType? PropertyType { get; set; }
        public PropertyNature? PropertyNature { get; set; } // شقة، فيلا، شاليه، سطح، غرفة، سرير
        public RentType? Type { get; set; } // سكني، طلاب
        public AvailabilityStatus? AvailabilityStatus { get; set; } // جاهز أو قيد التسليم
        public string? Rooms { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Floor { get; set; } // "الارضي", "1", "2", ...
        public bool? HasWifi { get; set; }
        public ThereIsInstallment? ThereIsInstallment { get; set; } = null;
    }
}
