using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "El campo Name es obligatorio")]
        public string Name { get; set; }
        [StringLength(200, ErrorMessage = "El ancho del campo es muy largo")]
        public string Description { get; set; }
    }
}
