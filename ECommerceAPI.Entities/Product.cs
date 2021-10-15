using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Entities
{
    public class Product : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public decimal UnitPrice { get; set; }

        [StringLength(1000)]
        public string ProductUrl { get; set; }

        public bool Active { get; set; }
    }
}
