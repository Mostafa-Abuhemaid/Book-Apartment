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

namespace Web.Application.Features.Reviews.Commands.UpdateReview
{
	public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, BaseResponse<List<string>>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<UpdateReviewCommand> _validator;
		private readonly IHttpContextAccessor _contextAccessor;

		public UpdateReviewCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IValidator<UpdateReviewCommand> validator, IHttpContextAccessor contextAccessor)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_validator = validator;
			_contextAccessor = contextAccessor;
		}

		public async Task<BaseResponse<List<string>>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);
			if (!validationResult.IsValid)
			{
				return new BaseResponse<List<string>>(false, "Validation Error", validationResult.Errors.Select(x => x.ErrorMessage).ToList());
			}

			var review = await _unitOfWork.Repository<int, PropertyReview>().GetByIdAsync(request.Id);
			if (review == null)
			{
				return new BaseResponse<List<string>>(false, "Review not found!");
			}

			var user = await GetUser.GetCurrentUserAsync(_contextAccessor, _userManager);
			if (user == null || user.Id != review.UserId)
			{
				return new BaseResponse<List<string>>(false, "User UnAuthorized!");
			}

			review.Comment = request.Comment;
			review.Stars= request.Stars;
			await _unitOfWork.SaveChangesAsync();

			return new BaseResponse<List<string>>(true, "Your review updated successfully!");

		}
	}
}
