using Admin.DTO;
using Order.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Intfs
{
    public interface IOrderService
    {
        public Task<ResultDTO> CreateOrder(string token, OrderDTO input);
        public Task<ResultDTO> CreateOrderDetail(string OrderId, List<OrderDetailDTO> input);
        public Task<ResultDTO> CheckoutOnl(string input);
        public Task<ResultDTO> GetStatus(string token, string id);
        public List<OrderNewDTO> GetNew();
        public List<OrderDTO> GetOrders(string token);
    }
}
