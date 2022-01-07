using ECommerceAPI.Entities;
using ECommerceAPI.Services.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Services
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            return services.AddAutoMapper(options =>
            {
                options.AddMaps(typeof(Category));
                options.AddMaps(typeof(Customer));
                options.AddMaps(typeof(Product));
                options.AddProfile<CategoryProfile>();
                options.AddProfile<CustomerProfile>();
                options.AddProfile<ProductProfile>();
                options.AddProfile<SaleProfile>();
            });
        }
    }
}
