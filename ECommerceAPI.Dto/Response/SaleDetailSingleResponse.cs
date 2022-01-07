﻿namespace ECommerceAPI.Dto.Response
{
    public class SaleDetailSingleResponse
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
