using ECommerceAPI.DataAccess;
using ECommerceAPI.DataAccess.Repositories;
using ECommerceAPI.Services.Implementations;
using ECommerceAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Services
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddInjectionDependency(this IServiceCollection services)
        {
            services.AddScoped<ECommerceDbContext>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}
