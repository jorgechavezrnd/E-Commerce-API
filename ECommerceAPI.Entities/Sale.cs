using System;

namespace ECommerceAPI.Entities
{
    public class Sale : EntityBase
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime SaleDate { get; set; }
        public string InvoiceNumber { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public decimal TotalSale { get; set; }
    }
}
