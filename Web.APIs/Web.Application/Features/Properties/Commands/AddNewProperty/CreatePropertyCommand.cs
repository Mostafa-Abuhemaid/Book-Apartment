using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Web.Application.Response;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Commands.AddNewProperty
{
    public record CreatePropertyCommand(
        string? Title,
        string? Description,
        PropertyNature Type,
        string? Rooms,
        string? Bathrooms,
        double? Area,
        int? Price,
        string? Floor,
        bool? IsFurnished,
        PropertyType PropertyType,
        string? Governorate,
        string? City,
        RentType? RentType,
        double? RentAdvance,
        double? RentPrice,
        string? PriceRentType,
        AvailabilityStatus? AvailabilityStatus,
        bool? HasWifi,
        bool IsActive,
        IFormFile? MainImage,
        string OwnerId,
        ICollection<IFormFile>? Images
    ) : IRequest<BaseResponse<string>>;
}
