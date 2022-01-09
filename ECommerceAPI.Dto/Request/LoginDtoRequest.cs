using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class LoginDtoRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
