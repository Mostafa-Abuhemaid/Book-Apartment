using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Features.Properties.Queries.GetFavorit;
using Web.Domain.Entites;

namespace Web.Application.Mapping
{
    public class PropertyMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Property, GetFavoritQueryDto>();
        }
    }
}
