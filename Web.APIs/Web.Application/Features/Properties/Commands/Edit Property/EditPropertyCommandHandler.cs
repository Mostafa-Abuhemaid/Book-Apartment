using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Response;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Commands.Edit_Property
{
    public class EditPropertyCommandHandler : IRequestHandler<EditPropertyCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;

        public EditPropertyCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> Handle(EditPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);

            if (property == null)
                return new BaseResponse<bool>(false, "العقار غير موجود");

          
            property.Title = request.Title ?? property.Title;
            property.Description = request.Description ?? property.Description;
            property.Type = request.Type ?? property.Type;
            property.Rooms = request.Rooms ?? property.Rooms;
            property.Bathrooms = request.Bathrooms ?? property.Bathrooms;
            property.Area = request.Area ?? property.Area;
            property.Price = request.Price ?? property.Price;
            property.Floor = request.Floor ?? property.Floor;
            property.IsFurnished = request.IsFurnished ?? property.IsFurnished;
            property.PropertyType = request.PropertyType ?? property.PropertyType;
            property.Governorate = request.Governorate ?? property.Governorate;
            property.City = request.City ?? property.City;
            property.RentType = request.RentType ?? property.RentType;
            property.RentAdvance = request.RentAdvance ?? property.RentAdvance;
            property.RentPrice = request.RentPrice ?? property.RentPrice;
            property.PriceRentType = request.PriceRentType ?? property.PriceRentType;
            property.AvailabilityStatus = request.AvailabilityStatus ?? property.AvailabilityStatus;
            property.HasWifi = request.HasWifi ?? property.HasWifi;
            property.IsActive = request.IsActive ?? property.IsActive;
            property.PropertySaleStatus = request.PropertySaleStatus ?? property.PropertySaleStatus;
            property.PropertyRentStatus = request.PropertyRentStatus ?? property.PropertyRentStatus;           
            property.ThereIsInstallment = request.ThereIsInstallment ?? property.ThereIsInstallment;

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<bool>(true, "تم تعديل العقار بنجاح", true);
        }
    }
}
