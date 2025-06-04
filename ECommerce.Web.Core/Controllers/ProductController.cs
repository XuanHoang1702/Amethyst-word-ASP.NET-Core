using Admin.DTO;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Product.DTO;
using Product.Intfs;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly IStockService _stockService;

        public ProductController(IProductService productService, IDiscountService discountService, IStockService stockService)
        {
            _productService = productService;
            _discountService = discountService;
            _stockService = stockService;
        }

        #region Product
        [HttpGet]
        public IActionResult ProductList(int pageNumber = 1, int pageSize = 10)
        {
            var result =  _productService.GetList(pageNumber, pageSize);
            return Ok(new
            {
                TotalRecords = result.TOTAL_RECORD,
                TotalPages = (int)Math.Ceiling((double)result.TOTAL_RECORD / pageSize),
                Data = result.DATA
            });
        }

        [HttpGet]
        public async Task<ProductDetailDTO> Detail(int input) => await _productService.Detail(input);

        [HttpGet]
        public IActionResult Search(string name, int pageNumber = 1, int pageSize = 10)
        {
            var result = _productService.Search(name, pageNumber, pageSize);
            return Ok(new
            {
                TotalRecords = result.TOTAL_RECORD,
                TotalPages = (int)Math.Ceiling((double)result.TOTAL_RECORD / pageSize),
                Data = result.DATA
            });
        }

        [HttpGet]
        public IActionResult Fillter(int brandId, int categoryId, decimal priceMin = 0, decimal pricaMax = 99999, int pageNumber = 1, int pageSize = 10)
        {
            var result = _productService.Fillter(brandId, categoryId, pageNumber, pageSize, priceMin, pricaMax);
            return Ok(new
            {
                TotalRecords = result.TOTAL_RECORD,
                TotalPages = (int)Math.Ceiling((double)result.TOTAL_RECORD / pageSize),
                Data = result.DATA
            });
        }

        [HttpPost]
        public async Task<ResultDTO> Create([FromHeader(Name = "Authorization")] string token, [FromBody] ProductCreateDTO input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _productService.Create(token, input);
        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader(Name = "Authorization")] string token, int id)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _productService.Delete(token, id);
        }

        [HttpPut]
        public async Task<ResultDTO> Update([FromHeader(Name = "Authorization")] string token, [FromBody] ProductUpdateDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _productService.Update(token, input);
        }

        [HttpGet]
        public async Task<List<ProductDTO>> Related(int input) => await _productService.Related(input);

        [HttpGet]
        public async Task<List<ProductDTO>> ProductNew(int input) => await _productService.ProductNew(input);

        [HttpGet]
        public async Task<List<ProductDTO>> ProductBestSeller(int input) => await _productService.ProductBestSeller(input);

        [HttpGet]
        public List<ColorDTO> GetColor(int id) => _productService.GetColor(id);

        [HttpGet]
        public List<SizeDTO> getSize(int id) => _productService.GetSize(id);

        #endregion

        #region Image
        [HttpPost]
        public async Task<ResultDTO> CreateImage([FromHeader(Name = "Authorization")] string token, [FromBody] ImageDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _productService.CreateImage(token, input);
        }
        #endregion

        #region Discount
        [HttpGet]
        public IActionResult DiscoutList(int pageNumber, int pageSize)
        {
            var result = _discountService.GetList(pageNumber, pageSize);
            return Ok(new
            {
                TotalRecords = result.TOTAL_RECORD,
                TotalPages = (int)Math.Ceiling((double)result.TOTAL_RECORD / pageSize),
                Data = result.DATA
            });
        }

        [HttpPost]
        public async Task<object> CreeateDiscount([FromHeader(Name = "Authorization")] string token, [FromBody] DiscountInputDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _discountService.Create(token, input);
        }

        [HttpPut]
        public async Task<object> UpdateDiscount([FromHeader(Name = "Authorization")] string token, [FromBody] DiscountInputDTO input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _discountService.Update(token, input);
        }

        [HttpDelete]
        public async Task<ResultDTO> DeleteDiscount([FromHeader(Name = "Authorization")] string token, [FromBody] int input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _discountService.Delete(token, input);
        }

        [HttpGet]
        public async Task<List<DiscountDTO>> DiscountHome(int input) => await _discountService.Home(input);

        #endregion

        #region Stock
        [HttpGet]
        public List<StockDTO> GetStocks() => _stockService.StockList();

        #endregion

        #region Rate Comment

        [HttpPost]
        public async Task<ResultDTO> Create_Rate_Comment([FromHeader(Name = "Authorization")] string token,[FromBody] RateCommentDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _productService.RateComment(token, input);
        }

        [HttpGet]
        public async Task<List<RateCommentDTO>> Get_Rate_Comment(int id) => await _productService.GetRateComment(id);

        #endregion

    }
}
