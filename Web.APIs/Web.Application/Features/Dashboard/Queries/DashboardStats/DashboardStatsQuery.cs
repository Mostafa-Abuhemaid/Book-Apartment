using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Response;
using Web.Domain.DTOs.Dashboard;

namespace Web.Application.Features.Dashboard.Queries.DashboardStats
{
    public class DashboardStatsQuery : IRequest<BaseResponse<DashboardStatsDto>>
    {
    }
}
