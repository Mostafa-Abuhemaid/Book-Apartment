using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Reviews.Commands.AddReview;
using Web.Application.Helpers;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.Reviews.Commands.DeleteReview
{
	public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, BaseResponse<int>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _contextAccessor;

		public DeleteReviewCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_contextAccessor = contextAccessor;
		}

		public async Task<BaseResponse<int>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
		{
			if (request.Id== null)
			{
				return new BaseResponse<int>(false, "Invalid Id");
			}

			var user = await GetUser.GetCurrentUserAsync(_contextAccessor, _userManager);
			if (user == null)
			{
				return new BaseResponse<int>(false, "User UnAuthorized!");
			}

			var review = await _unitOfWork.Repository<int, PropertyReview>().GetByIdAsync(request.Id);
			if (review == null)
			{
				return new BaseResponse<int>(false, "Review not found!");
			}

			if(review.UserId != user.Id)
			{
				return new BaseResponse<int>(false, "You not have permission for removing this review!");
			}

			_unitOfWork.Repository<int, PropertyReview>().Remove(review);
			await _unitOfWork.SaveChangesAsync();

			return new BaseResponse<int>(true, "Your review removed successfully!");
		}
	}
}
