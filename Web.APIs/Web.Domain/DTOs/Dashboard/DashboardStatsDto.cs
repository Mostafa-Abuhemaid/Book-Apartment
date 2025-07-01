using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public int TotalUnits { get; set; }
        public int RentedUnits { get; set; }
        public int SoldUnits { get; set; }
        public int TotalUsers { get; set; }
        public int BlockedUsers { get; set; }
    }
}
