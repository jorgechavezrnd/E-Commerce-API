using ECommerceAPI.DataAccess;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ECommerceAPI.UnitTests
{
    public class DbContextUnitTest : IDisposable
    {
        protected readonly ECommerceDbContext Context;

        protected DbContextUnitTest()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new ECommerceDbContext(options);

            Context.Database.EnsureCreated();

            Seed(Context);
        }

        private void Seed(ECommerceDbContext context)
        {
            var list = new List<Customer>();
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Customer
                {
                    Name = $"Cliente {i}",
                    BirthDate = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28)),
                    Dni = $"243534{i * 20}",
                    LastName = $"Apellido {random.Next()}"
                });
            }

            Context.Set<Customer>().AddRange(list);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
