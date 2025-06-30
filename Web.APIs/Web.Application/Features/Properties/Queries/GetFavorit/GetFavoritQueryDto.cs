using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Features.Properties.Queries.GetFavorit
{
	public record GetFavoritQueryDto(
	int Id,
	string? Title,	
    double? Area,
    string? Floor,
    string? Governorate,
    string? City,
    int? Price ,
    string? MainImage ,
    string? Rooms 
				
		);
}
