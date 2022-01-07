namespace ECommerceAPI.Dto.Response
{
    public class SaleDtoSingleResponse
    {
        public string SaleId { get; set; }
        public string InvoiceNumber { get; set; }
        public string SaleDate { get; set; }
        public string Customer { get; set; }
        public decimal SaleTotal { get; set; }
    }
}
