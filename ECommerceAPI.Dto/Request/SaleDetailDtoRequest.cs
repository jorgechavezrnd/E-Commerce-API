namespace ECommerceAPI.Dto.Request
{
    public class SaleDetailDtoRequest
    {
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}