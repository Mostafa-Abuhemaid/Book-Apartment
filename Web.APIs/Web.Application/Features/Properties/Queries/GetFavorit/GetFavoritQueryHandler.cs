using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using Web.Infrastructure.Data;

namespace Web.Application.Features.Properties.Queries.GetFavorit
{
	public class GetFavoritQueryHandler : IRequestHandler<GetFavoritQuery, BaseResponse<List<GetFavoritQueryDto>>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly AppDbContext _dbContext;

        public GetFavoritQueryHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, AppDbContext dbContext)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<List<GetFavoritQueryDto>>> Handle(GetFavoritQuery request, CancellationToken cancellationToken)
		{
			if (request.userId == null)
			{
				return new BaseResponse<List<GetFavoritQueryDto>>(false, "Invalid Id");
			}

			var user = await GetUser.GetCurrentUserAsync(_contextAccessor, _userManager);
			if (user == null || request.userId != user.Id)
			{
				return new BaseResponse<List<GetFavoritQueryDto>>(false, "User UnAuthorized!");
			}

			var favorits = await _dbContext.Favorites.Where(f => f.UserId == request.userId).ToListAsync();
			if (favorits==null || !favorits.Any())
			{
				return new BaseResponse<List<GetFavoritQueryDto>>(false, "You not have favorit properties!");
			}

			var response = new List<GetFavoritQueryDto>();
			foreach (var i in favorits)
			{
				var property=await _unitOfWork.Repository<int,Property>().GetByIdAsync(i.PropertyId);
				if(property != null)
				{
					response.Add(property.Adapt<GetFavoritQueryDto>());
				}
			}

			return new BaseResponse<List<GetFavoritQueryDto>>(true, "Your favorit properties retrived successfully!", response);
		}
	}
}
