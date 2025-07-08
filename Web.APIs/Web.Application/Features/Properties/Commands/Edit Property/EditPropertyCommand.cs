using MediatR;
using Web.Application.Response;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Commands.Edit_Property
{
    public class EditPropertyCommand : IRequest<BaseResponse<bool>>
    {
        public int PropertyId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public PropertyNature Type { get; set; }
        public string? Rooms { get; set; }
        public string? Bathrooms { get; set; }
        public double? Area { get; set; }
        public int? Price { get; set; }
        public string? Floor { get; set; }
        public bool? IsFurnished { get; set; }
        public PropertyType PropertyType { get; set; }
        public string? Governorate { get; set; }
        public string? City { get; set; }
        public RentType? RentType { get; set; }
        public double? RentAdvance { get; set; }
        public double? RentPrice { get; set; }
        public string? PriceRentType { get; set; }
        public AvailabilityStatus? AvailabilityStatus { get; set; }
        public bool? HasWifi { get; set; }
        public bool? IsActive { get; set; }
        public PropertySaleStatus? PropertySaleStatus { get; set; }
        public PropertyRentStatus? PropertyRentStatus { get; set; }
       
        public ThereIsInstallment? ThereIsInstallment { get; set; }
    }
}
