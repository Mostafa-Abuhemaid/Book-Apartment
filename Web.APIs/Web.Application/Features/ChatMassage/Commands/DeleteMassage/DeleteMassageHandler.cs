using FirebaseAdmin.Messaging;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.ChatMassage.Commands.DeleteMassage
{
    public class DeleteMassageHandler : IRequestHandler<DeleteMassageCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMassageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<bool>> Handle(DeleteMassageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.Repository<int,ChatMessage >().GetByIdAsync(request.MessageId);
            if (message == null)
            {
                return new BaseResponse<bool>(false, "Massage was deleted"); 
            }

            _unitOfWork.Repository<int,ChatMessage>().Remove(message);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Message deleted");
        }
    }
}
