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

        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$")]
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
