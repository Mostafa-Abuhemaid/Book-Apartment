using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Properties.Queries.GetFavorit;
using Web.Domain.DTOs.PropertyDTO;
using Web.Domain.Entites;

namespace Web.Application.Mapping
{
    public class PropertyMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Property, GetFavoritQueryDto>()
             .Map(dest => dest.MainImage,
         src => string.IsNullOrEmpty(src.MainImage)
             ? null
             : $"http://realestateunits.runasp.net/Property/{src.MainImage}");
            ///////////////////////////////////////////////////////////////////
            config.NewConfig<Property, GetPropertyDto>()
                .Map(dest => dest.Images,
         src => src.Images
                  .Where(img => !string.IsNullOrEmpty(img.ImageUrl)) 
                  .Select(img => $"http://realestateunits.runasp.net/Property/{img.ImageUrl}")
     )
    .AfterMapping((src, dest) =>
    {
        dest.MainImage = string.IsNullOrEmpty(src.MainImage)
            ? null
            : $"http://realestateunits.runasp.net/Property/{src.MainImage}";
    });
       ////////////////////////////////////////////////////////////////
       
       config.NewConfig<Property, GetAllPropertiesDto>()
    .Map(dest => dest.MainImage,
         src => string.IsNullOrEmpty(src.MainImage)
             ? null
             : $"http://realestateunits.runasp.net/Property/{src.MainImage}");

        }
    }
}
