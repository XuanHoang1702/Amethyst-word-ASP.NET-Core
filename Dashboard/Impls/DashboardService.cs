using Dashboard.Intfs;
using Application.Share;
using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using Dashboard.DTO;
using Admin.DTO;

namespace Dashboard.Impls
{
    public class DashboardService : IDashboardService
    {
        private readonly IGetListData _lstData;

        public  DashboardService(IGetListData lstData)
        {
            _lstData = lstData;
        }

        public List<DashboardDTO> GetUser()
        {
            return _lstData.ExecuteGetListData<DashboardDTO>(StoreProcedureConsts.USER_Quantity);
        }

        public List<DashboardDTO> GetOrder()
        {
            return _lstData.ExecuteGetListData<DashboardDTO>(StoreProcedureConsts.ORDER_Quantity);
        }

        public List<DashboardDTO> GetRevenueByYear(DateTime input)
        {
            return _lstData.ExeciteGetListDataById<DashboardDTO>(StoreProcedureConsts.ORDER_Revenue, new  { p_DATE = input});
        }

        public List<DashboardDTO> GetRevenueByMonth(DateTime input)
        {
            return _lstData.ExeciteGetListDataById<DashboardDTO>(StoreProcedureConsts.REVENUE_Month, new { p_DATE = input });
        }

        public List<DashboardDTO> GetRevenueByWeek(DateTime input)
        {
            return _lstData.ExeciteGetListDataById<DashboardDTO>(StoreProcedureConsts.REVENUE_Week, new { p_DATE = input });
        }

        public List<DashboardDTO> GetRevenueTotal()
        {
            return _lstData.ExecuteGetListData<DashboardDTO>(StoreProcedureConsts.REVENUE_Total);
        }
    }
}
