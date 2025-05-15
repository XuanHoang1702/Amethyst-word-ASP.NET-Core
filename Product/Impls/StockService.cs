using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using Product.DTO;
using Product.Intfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Impls
{
    public class StockService : IStockService
    {
        private readonly IGetListData _listData;
        public StockService(IGetListData listData)
        {  
            _listData = listData;
        }

        public List<StockDTO> StockList()
        {
            return _listData.ExecuteGetListData<StockDTO>(StoreProcedureConsts.STOCK_List);
        }
    }
}
