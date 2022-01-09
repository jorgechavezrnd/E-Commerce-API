using ECommerceAPI.Entities;
using System;
using System.Collections.Generic;

namespace ECommerceAPI.Dto.Response
{
    public class LoginDtoResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public List<MenuOption> MenuOptions { get; set; }
    }
}
