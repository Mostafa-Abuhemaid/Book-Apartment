using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Commands.ApproveProperty
{
    public class ApprovePropertyHandler : IRequestHandler<ApprovePropertyCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;

        public ApprovePropertyHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> Handle(ApprovePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties.FindAsync(request.PropertyId);

            if (property == null)
                return new BaseResponse<bool>(false, "العقار غير موجود");

            property.IsActive = true;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<bool>(true, "تم قبول الوحدة بنجاح");
        }
    }
}