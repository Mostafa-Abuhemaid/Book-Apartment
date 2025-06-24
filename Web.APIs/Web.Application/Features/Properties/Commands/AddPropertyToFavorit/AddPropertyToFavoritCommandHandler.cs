using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Application.Helpers;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Repositories;
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Commands.AddPropertyToFavorit
{
	public class AddPropertyToFavoritCommandHandler : IRequestHandler<AddPropertyToFavoritCommand, BaseResponse<int>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _dbContext;
        public AddPropertyToFavoritCommandHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
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
				return new BaseResponse<int>(false, "العقار غير موجود");
			}

			var isFavorit =   await _dbContext.Favorites.FirstOrDefaultAsync(f => f.UserId == user.Id && f.PropertyId== request.PropertyId);
            if (isFavorit != null)
			{
				_dbContext.Remove(isFavorit);	
				await _unitOfWork.SaveChangesAsync();
				return new BaseResponse<int>(true, $"تم ازالة هذا العقار من المفضلة");
			}
            var Favorit = new Favorite { PropertyId = request.PropertyId, UserId = user.Id };
            await _dbContext.AddAsync(Favorit);
            await _unitOfWork.SaveChangesAsync();

			return new BaseResponse<int>(true, $"تم اضافة هذا العقار للمفضلة");


		}
	}
}
