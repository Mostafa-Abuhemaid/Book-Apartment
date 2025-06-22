using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Files;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.Properties.Commands.AddNewProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreatePropertyCommand> _validator;
        public CreatePropertyCommandHandler(IUnitOfWork unitOfWork, IValidator<CreatePropertyCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BaseResponse<string>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new BaseResponse<string>(false, "Validation Error", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            var property = request.Adapt<Property>();
            if (request.MainImage != null)
                property.MainImage = Media.UploadFile(request.MainImage, "Property");

            await _unitOfWork.Repository<int, Property>().AddAsync(property);
            await _unitOfWork.SaveChangesAsync();
            foreach (var image in request.Images)
            {
                var imageUrl = Media.UploadFile(image, "Property");
                var imgEntity = new PropertyImage
                {
                    PropertyId = property.Id,
                    ImageUrl = imageUrl
                };
                await _unitOfWork.Repository<int, PropertyImage>().AddAsync(imgEntity);
            }

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<string>(true, "Property created successfully!");
        }
    }
}
