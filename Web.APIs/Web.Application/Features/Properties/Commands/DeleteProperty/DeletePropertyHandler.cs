using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Files;
using Web.Application.Response;
using Web.Infrastructure.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyHandler : IRequestHandler<DeletePropertyCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;

        public DeletePropertyHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties
                   .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);
          
            if (property == null)
                return new BaseResponse<bool>(false, "العقار غير موجود");


            if (property.Images != null && property.Images.Any())
            {
                 _context.PropertyImages.RemoveRange(property.Images);        
            foreach (var image in property.Images)
                    if (!string.IsNullOrEmpty(image.ImageUrl))
                         Media.DeleteFile(image.ImageUrl, "Property");
            }
            if (property.MainImage != null)
                Media.DeleteFile(property.MainImage, "Property");


            _context.Properties.Remove(property);
            await _context.SaveChangesAsync(cancellationToken);
           

            return new BaseResponse<bool>(true, "تم حذف العقار بنجاح");
        }
    }
}

