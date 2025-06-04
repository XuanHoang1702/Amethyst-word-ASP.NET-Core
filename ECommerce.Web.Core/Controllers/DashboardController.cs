using Dashboard.DTO;
using Dashboard.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboard;
        public DashboardController(IDashboardService dashboardService) 
        {
            _dashboard = dashboardService;
        }

        [HttpGet]
        public List<DashboardDTO> GetUser() => _dashboard.GetUser();

        [HttpGet]
        public List<DashboardDTO> Getorder() => _dashboard.GetOrder();

        [HttpGet]
        public List<DashboardDTO> GetRevenueByYear(DateTime input) => _dashboard.GetRevenueByYear(input);

        [HttpGet]
        public List<DashboardDTO> GetRevenueByMonth(DateTime input) => _dashboard.GetRevenueByMonth(input);

        [HttpGet]
        public List<DashboardDTO> GetRevenueByWeek(DateTime input) => _dashboard.GetRevenueByWeek(input);

        [HttpGet]
        public List<DashboardDTO> GetRevenueTotal() => _dashboard.GetRevenueTotal();
    }
}
