using Microsoft.AspNetCore.Identity;
using System;

namespace ECommerceAPI.DataAccess
{
    public class ECommerceUserIdentity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
