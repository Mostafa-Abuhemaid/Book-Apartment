using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.PropertyDTO;
using Web.Application.Features.Properties.Queries.GetFavorit;
using Web.Domain.Entites;

namespace Web.Application.Mapping
{
	public class FavoritMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
            TypeAdapterConfig<CreatePropertyDto, Property>.NewConfig()
                  .Ignore(dest => dest.Id)
                  .Ignore(dest => dest.MainImage)
                  .Ignore(dest => dest.CreatedAt)
                  .Ignore(dest => dest.Images) 
                  .Ignore(dest => dest.PropertyReviews); 
        }
	}
}
