namespace ECommerceAPI.Entities.Complex
{
    public class InvoiceInfo
    {
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
