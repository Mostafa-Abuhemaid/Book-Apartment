using Mapster;
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

namespace Web.Application.Features.Reviews.Queries.GetReviews
{
	public class GetPropertyReviewsQueryHandler : IRequestHandler<GetPropertyReviewsQuery, BaseResponse<List<GetPropertyReviewsQueryDto>>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;

		public GetPropertyReviewsQueryHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}

		public async Task<BaseResponse<List<GetPropertyReviewsQueryDto>>> Handle(GetPropertyReviewsQuery request, CancellationToken cancellationToken)
		{
			if (request.PropertyId == null)
			{
				return new BaseResponse<List<GetPropertyReviewsQueryDto>>(false, "InValid Property Id!");
			}

			var property = await _unitOfWork.Repository<int, Property>().GetByIdAsync(request.PropertyId);
			if (property == null)
			{
				return new BaseResponse<List<GetPropertyReviewsQueryDto>>(false, "Property not found!");
			}

			var reveiws=await _unitOfWork.Repository<int,PropertyReview>().GetWithPrdicateAsync(x=>x.PropertyId == request.PropertyId);
			var response = new List<GetPropertyReviewsQueryDto>();
			foreach(var i in reveiws)
			{
				var review=i.Adapt<GetPropertyReviewsQueryDto>();
				var user = await _userManager.FindByIdAsync(i.UserId);
				review.reviewer = user.Adapt<ReviewerDto>();
				response.Add(review);
			}

			return new BaseResponse<List<GetPropertyReviewsQueryDto>>(true, "Reviews with reviewers", response);
		}
	}
}
