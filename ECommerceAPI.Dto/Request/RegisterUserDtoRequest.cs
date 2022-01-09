using ECommerceAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Dto.Request
{
    public class RegisterUserDtoRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(Constants.DatePattern)]
        public string BirthDate { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserCode { get; set; }
    }
}
