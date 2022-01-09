using System.Collections.Generic;

namespace ECommerceAPI.Dto.Response
{
    public class RegisterUserDtoResponse
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
