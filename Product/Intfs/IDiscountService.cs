using Admin.DTO;
using Application.Share.Consts.DTO;
using Product.DTO;

namespace Product.Intfs
{
    public interface IDiscountService
    {
        public PaginateResult<DiscountDTO> GetList(int pageNumber, int pageSize);
        public Task<object> Create(string token, DiscountInputDTO input);
        public Task<object> Update(string token, DiscountInputDTO input);
        public Task<ResultDTO> Delete(string token, int input);
        public Task<List<DiscountDTO>> Home(int record);
    }
}
