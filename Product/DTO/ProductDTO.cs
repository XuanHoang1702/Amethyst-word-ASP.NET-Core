namespace Product.DTO
{
    public class ProductDTO
    {
        public int PRODUCT_ID { get; set; }
        public string? PRODUCT_NAME {  get; set; }
        public string? IMAGE_NAME { get; set; }
        public decimal? PRODUCT_PRICE { get; set; }
        public string? PRODUCT_DESCRIPTION { get; set; }
        public string? PRODUCT_DETAIL { get; set; } 
        public decimal RATE {  get; set; }
        public int CATEGORY_ID { get; set; }
        public int BRAND_ID { get; set; }
        public decimal? DISCOUNT_PERCENT { get; set; }
        public string? PRODUCT_STATUS { get; set; }
        public int QUANTITY { get; set; }
    }
}
