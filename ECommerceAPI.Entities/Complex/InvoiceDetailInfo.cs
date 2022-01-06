namespace ECommerceAPI.Entities.Complex
{
    public class InvoiceDetailInfo
    {
        public string Id { get; set; }
        public int ItemNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
