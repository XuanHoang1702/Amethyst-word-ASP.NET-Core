using Admin.DTO;
using Dashboard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Intfs
{
    public interface IDashboardService
    {
        public List<DashboardDTO> GetUser();
        public List<DashboardDTO> GetOrder();
        public List<DashboardDTO> GetRevenueByYear(DateTime input);
        public List<DashboardDTO> GetRevenueByMonth(DateTime input);
        public List<DashboardDTO> GetRevenueByWeek(DateTime input);
        public List<DashboardDTO> GetRevenueTotal();
    }
}
