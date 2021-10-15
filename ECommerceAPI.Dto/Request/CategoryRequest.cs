using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "El campo Name es obligatorio")]
        public string Name { get; set; }
        [StringLength(20, ErrorMessage = "El ancho del campo es muy largo")]
        public string Description { get; set; }
    }
}
