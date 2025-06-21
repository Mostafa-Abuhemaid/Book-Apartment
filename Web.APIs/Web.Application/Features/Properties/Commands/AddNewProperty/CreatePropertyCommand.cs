using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.PropertyDTO;
using Web.Application.Response;
using Web.Domain.Enums;

namespace Web.Application.Features.Properties.Commands.AddNewProperty
{
    public record CreatePropertyCommand(string Title,
        string? Description,
        PropertyNature Type,
        int? Rooms,
        int? Bathrooms,
        double? Area,
        int Price,
        int? Floor,
        PropertyType PropertyType,
        string? Address,
        RentType? RentType,
        AvailabilityStatus? AvailabilityStatus,
        bool? HasWifi,
        IFormFile? MainImage,
        string OwnerId,
        ICollection<IFormFile> Images) : IRequest<BaseResponse<string>>;
}
