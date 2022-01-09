using System.Collections.Generic;

namespace ECommerceAPI.Entities
{
    public class AppSettings
    {
        public StorageConfiguration StorageConfiguration { get; set; }
        public ICollection<MenuOption> AdminOptions { get; set; }
        public ICollection<MenuOption> CustomerOptions { get; set; }
    }

    public class StorageConfiguration
    {
        public string Path { get; set; }
        public string PublicUrl { get; set; }
    }
}
