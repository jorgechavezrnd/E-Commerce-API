using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class ProductDtoRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string UrlImage { get; set; }

        public string Base64Image { get; set; }

        public string FileName { get; set; }

        [Range(1, 100000)]
        public decimal UnitPrice { get; set; }

        public bool Active { get; set; }
    }
}
