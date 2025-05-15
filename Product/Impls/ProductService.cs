using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Application.Share.Consts.DTO;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using Product.DTO;
using Product.Intfs;
using System.Text.Json;

namespace Product.Impls
{
    public class ProductService : IProductService
    {
        private readonly IGetListData _lstData;
        private readonly IGetData _getData;
        private readonly IFunction _function;
        private readonly ICreateData _createData;
        private readonly IDeleteData _deleteData;
        private readonly IUpdateData _updateData;
        private readonly IGetListData _getListData;

        public ProductService(IGetListData lstData, IGetData getData, IFunction function, ICreateData createData, IDeleteData deleteData, IUpdateData updateData, IGetListData getListData)
        {
            _lstData = lstData;
            _getData = getData;
            _createData = createData;
            _function = function;
            _deleteData = deleteData;
            _updateData = updateData;
            _getListData = getListData;
        }

        public PaginateResult<ProductDTO> GetList(int pageNumber, int pageSize)
        {
            var jsonData = new
            {
                PAGE_NUMBER = pageNumber,
                PAGE_SIZE = pageSize,
            };
            string jsonString = JsonConvert.SerializeObject(jsonData);

            var result = _lstData.ExecutePaginateData<ProductDTO>(StoreProcedureConsts.PRODUCT_List, new { p_PRODUCT_DATA_JSON = jsonString });
            return result;
        }

        public async Task<object> Detail(int id)
        {
            var rawData =  _getListData.ExeciteGetListDataById<ProductDetailDTO>(StoreProcedureConsts.PRODUCT_Detail, new { p_PRODUCT_ID = id });
            if (rawData == null || !rawData.Any())
            {
                return new { data = (object?)null };
            }
            var first = rawData.First();

            var result = new ProductDetailDTO
            {
                PRODUCT_ID = id,
                PRODUCT_NAME = first.PRODUCT_NAME,
                IMAGE_NAME = first.IMAGE_NAME,
                PRODUCT_PRICE = first.PRODUCT_PRICE,
                PRODUCT_DETAIL = first.PRODUCT_DETAIL,
                PRODUCT_DESCRIPTION = first.PRODUCT_DESCRIPTION,
                QUANTITY_TOTAL = first.QUANTITY_TOTAL,
                PRODUCT_STATUS = "Active",
                CREATED_AT = DateTime.Now,
                UPDATED_AT = DateTime.Now,
                Variants = rawData.Select(x => new ProductVariantDTO
                {
                    COLOR_NAME = x.COLOR_NAME,
                    SIZE_NAME = x.SIZE_NAME,
                    QUANTITY_COLOR = x.QUANTITY_COLOR,
                    QUANTITY_SIZE = x.QUANTITY_SIZE
                }).ToList()
            };

            return new { data = result };
        }

        public PaginateResult<ProductDTO> Search(string name, int pageNumber, int pageSize)
        {
            var jsonData = new
            {
                PAGE_NUMBER = pageNumber,
                PAGE_SIZE = pageSize,
                PRODUCT_NAME = name,
            };
            string jsonString = JsonConvert.SerializeObject(jsonData);

            var result = _lstData.ExecutePaginateData<ProductDTO>(StoreProcedureConsts.PRODUCT_Search, new { p_PRODUCT_DATA_JSON = jsonString });
            return result;
        }

        public PaginateResult<ProductDTO> Fillter(int brandId, int CategoryId, int pageNumber, int pageSixe, decimal priceMin, decimal priceMax)
        {
            var json = new
            {
                BRAND_ID = brandId,
                CATEGORY_ID = CategoryId,
                PAGE_NUMBER = pageNumber,
                PAGE_SIZE = pageSixe,
                PRICE_MIN = priceMin,
                PRICE_MAX = priceMax
            };
            string jsonString = JsonConvert.SerializeObject(json);

            var result = _lstData.ExecutePaginateData<ProductDTO>(StoreProcedureConsts.PRODUCT_Fillter, new { p_PRODUCT_DATA_JSON = jsonString });
            return result;
        }

        public async Task<object> Create(string token, ProductCreateDTO input)
        {
            var id = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = id,
                input.PRODUCT_NAME,
                input.PRODUCT_PRICE,
                input.PRODUCT_DETAIL,
                input.PRODUCT_DESCRIPTION,
                input.BRAND_ID,
                input.CATEGORY_ID,
                input.PRODUCT_STATUS
            };

            var param = JsonConvert.SerializeObject(json);
            ProductDetailDTO result = await _createData.ExcuteCreateData<ProductDetailDTO>(StoreProcedureConsts.PRODUCT_Create, new { p_PRODUCT_DATA_JSON = param });

            return new { code = 201, data = result };
        }

        public async Task<ResultDTO> Delete(string token, int id)
        {
            var AdminId = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = AdminId,
                PRODUCT_ID = id
            };

            var param = JsonConvert.SerializeObject(json);
            var result = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.PRODUCT_Delete, new { p_PRODUCT_DATA_JSON = param });

            return result;
        }

        public async Task<object> Update(string token, ProductUpdateDTO input)
        {
            var id = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = id,
                input.PRODUCT_ID,
                input.PRODUCT_NAME,
                input.PRODUCT_PRICE,
                input.PRODUCT_DETAIL,
                input.PRODUCT_DESCRIPTION,
                input.BRAND_ID,
                input.CATEGORY_ID,
                input.PRODUCT_STATUS
            };

            var param = JsonConvert.SerializeObject(json);
            var result = await _updateData.ExceuteUpdateData<ProductDetailDTO>(StoreProcedureConsts.PRODUCT_Update, new { p_PRODUCT_DATA_JSON = param });

            return new { data = result };
        }

        public async Task<List<ProductDTO>> Related(int input)
        {
            var result = _lstData.ExeciteGetListDataById<ProductDTO>(StoreProcedureConsts.PRODUCT_Related, new { p_PRODUCT_ID = input });
            return result;
        }

        public async Task<List<ProductDTO>> ProductNew(int quantity)
        {
            return  _lstData.ExecuteGetRecordData<ProductDTO>(StoreProcedureConsts.PRODUCT_New, new { p_RECORD = quantity });
        }

        public async Task<List<ProductDTO>> ProductBestSeller(int quantity)
        {
            return _lstData.ExecuteGetRecordData<ProductDTO>(StoreProcedureConsts.PRODUCT_BestSeller, new { p_RECORD = quantity });
        }
    }
}
