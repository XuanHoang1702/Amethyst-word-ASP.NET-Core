using Product.DTO;
using Application.Share.Consts.DTO;
using Admin.DTO;

namespace Product.Intfs
{
    public interface IProductService
    {
        public PaginateResult<ProductDTO> GetList(int pageNumber, int pageSize);
        public Task<ProductDetailDTO> Detail(int id);
        public PaginateResult<ProductDTO> Search(string name, int pageNumber, int pageSize);
        public PaginateResult<ProductDTO> Fillter(int brandId, int CategoryId, int pageNumber, int pageSixe, decimal priceMin, decimal priceMax);
        public Task<ResultDTO> Create(string token, ProductCreateDTO input);
        public Task<ResultDTO> Delete(string token, int id);
        public Task<ResultDTO> Update(string token, ProductUpdateDTO input);
        public Task<List<ProductDTO>> Related(int input);
        public Task<List<ProductDTO>> ProductNew(int quantity);
        public Task<List<ProductDTO>> ProductBestSeller(int quantity);
        public Task<ResultDTO> CreateImage(string token, ImageDTO input);
        public List<ColorDTO> GetColor(int id);
        public List<SizeDTO> GetSize(int id);
        public Task<ResultDTO> RateComment(string token, RateCommentDTO input);
        public Task<List<RateCommentDTO>> GetRateComment(int id);
    }
}
