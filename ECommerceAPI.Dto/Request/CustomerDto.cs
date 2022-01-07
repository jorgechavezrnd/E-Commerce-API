using ECommerceAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class CustomerDtoRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        [StringLength(500)]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        public string Email { get; set; }

        [RegularExpression(Constants.DatePattern)]
        public string BirthDate { get; set; }

        [Required]
        [StringLength(15)]
        public string Dni { get; set; }
    }

    public class CustomerDto : CustomerDtoRequest
    {
        public string Id { get; set; }
    }
}
