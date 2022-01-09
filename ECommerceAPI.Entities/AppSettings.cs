using System.Collections.Generic;

namespace ECommerceAPI.Entities
{
    public class AppSettings
    {
        public StorageConfiguration StorageConfiguration { get; set; }
        public ICollection<MenuOption> AdminOptions { get; set; }
        public ICollection<MenuOption> CustomerOptions { get; set; }
        public Jwt Jwt { get; set; }
    }

    public class StorageConfiguration
    {
        public string Path { get; set; }
        public string PublicUrl { get; set; }
    }

    public class Jwt
    {
        public string SigningKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
