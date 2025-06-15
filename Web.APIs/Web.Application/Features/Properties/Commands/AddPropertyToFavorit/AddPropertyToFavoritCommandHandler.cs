using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Application.Helpers;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;

namespace Web.Application.Features.Properties.Commands.AddPropertyToFavorit
{
	public class AddPropertyToFavoritCommandHandler : IRequestHandler<AddPropertyToFavoritCommand, BaseResponse<int>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;
		public AddPropertyToFavoritCommandHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
		}
		public async Task<BaseResponse<int>> Handle(AddPropertyToFavoritCommand request, CancellationToken cancellationToken)
		{
			if (request.PropertyId == null)
			{
				return new BaseResponse<int>(false, $"InValid property Id:{request.PropertyId}!");
			}

			var user = await GetUser.GetCurrentUserAsync(_contextAccessor, _userManager);
			if (user == null)
			{
				return new BaseResponse<int>(false, "User UnAuthorized!");
			}

			var property = await _unitOfWork.Repository<int, Property>().GetByIdAsync(request.PropertyId);
			if (property == null)
			{
				return new BaseResponse<int>(false, "Property not found!");
			}

			var isFavorit = await _unitOfWork.Repository<int, Favorite>().GetEntityWithPrdicateAsync(x => (x.PropertyId == property.Id && x.UserId == user.Id));
			if (isFavorit != null)
			{
				_unitOfWork.Repository<int, Favorite>().Remove(isFavorit);
				await _unitOfWork.SaveChangesAsync();
				return new BaseResponse<int>(true, $"Property: {property.Id} flagged as unfavorit!");
			}
			
			await _unitOfWork.Repository<int,Favorite>().AddAsync(isFavorit);
			await _unitOfWork.SaveChangesAsync();

			return new BaseResponse<int>(true, $"Property: {property.Id} flagged as favorit!");


		}
	}
}
