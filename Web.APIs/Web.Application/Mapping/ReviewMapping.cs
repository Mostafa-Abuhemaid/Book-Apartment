using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.ReviewDto;
using Web.Application.Features.Reviews.Queries.GetReviews;
using Web.Domain.Entites;

namespace Web.Application.Mapping
{
	public class ReviewMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<PropertyReview, GetReviewDto>();
			config.NewConfig<AppUser, ReviewerDto>();
		}
	}
}
