using Product.DTO;
using Application.Share.Consts.DTO;
using Admin.DTO;

namespace Product.Intfs
{
    public interface IProductService
    {
        public PaginateResult<ProductDTO> GetList(int pageNumber, int pageSize);
        public Task<object> Detail(int id);
        public PaginateResult<ProductDTO> Search(string name, int pageNumber, int pageSize);
        public PaginateResult<ProductDTO> Fillter(int brandId, int CategoryId, int pageNumber, int pageSixe, decimal priceMin, decimal priceMax);
        public Task<object> Create(string token, ProductCreateDTO input);
        public Task<ResultDTO> Delete(string token, int id);
        public Task<object> Update(string token, ProductUpdateDTO input);
        public Task<List<ProductDTO>> Related(int input);
        public Task<List<ProductDTO>> ProductNew(int quantity);
        public Task<List<ProductDTO>> ProductBestSeller(int quantity);
    }
}
