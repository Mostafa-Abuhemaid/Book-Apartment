using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Helpers;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.Reviews.Commands.AddReview
{
	public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, BaseResponse<List<string>>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<AddReviewCommand> _validator;
		private readonly IHttpContextAccessor _contextAccessor;

		public AddReviewCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IValidator<AddReviewCommand> validator, IHttpContextAccessor contextAccessor)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_validator = validator;
			_contextAccessor = contextAccessor;
		}

		public async Task<BaseResponse<List<string>>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
		{
			var validationResult= await _validator.ValidateAsync(request, cancellationToken);
			if (!validationResult.IsValid)
			{
				return new BaseResponse<List<string>>(false, "Validation Error", validationResult.Errors.Select(x => x.ErrorMessage).ToList());
			}

			var user = await GetUser.GetCurrentUserAsync(_contextAccessor, _userManager);
			if (user == null)
			{
				return new BaseResponse<List<string>>(false, "User UnAuthorized!");
			}

			var property = await _unitOfWork.Repository<int,Property>().GetByIdAsync(request.PropertyId);
			if(property == null)
			{
				return new BaseResponse<List<string>>(false, "Property not found!");
			}

			var review = new PropertyReview
			{
				PropertyId = property.Id,
				UserId = user.Id,
				Comment = request.Review,
				Stars=request.Stars,
				CreatedAt=DateTime.Now
			};

			await _unitOfWork.Repository<int,PropertyReview>().AddAsync(review);
			await _unitOfWork.SaveChangesAsync();

			return new BaseResponse<List<string>>(true,"Review added successfully!");
		}
	}
}
