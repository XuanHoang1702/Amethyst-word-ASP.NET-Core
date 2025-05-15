using Product.DTO;

namespace Product.Intfs
{
    public interface IStockService
    {
        public List<StockDTO> StockList();
    }
}
