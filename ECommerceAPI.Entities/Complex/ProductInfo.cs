namespace ECommerceAPI.Entities.Complex
{
    public class ProductInfo
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public string UrlProduct { get; set; }
        public bool Active { get; set; }
    }
}
