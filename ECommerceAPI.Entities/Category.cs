using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Entities
{
    public class Category : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
