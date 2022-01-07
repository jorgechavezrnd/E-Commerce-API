using ECommerceAPI.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class SaleDtoRequest
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        [RegularExpression(Constants.DatePattern)]
        public string Date { get; set; }

        public int PaymentMethod { get; set; }

        public ICollection<SaleDetailDtoRequest> Products { get; set; }
    }
}
