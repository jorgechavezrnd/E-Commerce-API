using AutoMapper;
using ECommerceAPI.DataAccess.Repositories;
using ECommerceAPI.Entities;
using ECommerceAPI.Services;
using ECommerceAPI.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerceAPI.UnitTests
{
    public class CustomerUnitTest : DbContextUnitTest
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            int a = 5;
            int b = 7;

            // Act

            var suma = a + b;
            var expected = 9;
            
            // Assert
            Assert.NotEqual(expected, suma);
        }

        [Fact]
        public void TestPaginacion()
        {
            // Arrange
            var total = 110;
            var rows = 10;

            // Act
            var resultado = ECommerceUtils.GetTotalPages(total, rows);
            var expected = 11;

            // Assert
            Assert.Equal(expected, resultado);
        }

        [Fact]
        public async Task TestCustomerPaginationTest()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.GetCollectionAsync(It.IsAny<string>(), 1, 10))
                .Returns(async () =>
                {
                    var tuple = (await Context.Set<Customer>().ToListAsync(),
                                 await Context.Set<Customer>().CountAsync());

                    return tuple;
                });

            var logger = new Mock<ILogger<CustomerService>>();
            var mapper = new Mock<IMapper>();

            var service = new CustomerService(repository.Object, logger.Object, mapper.Object);

            // Act

            var resultado = await service.GetCollectionAsync(It.IsAny<string>(), 1, 10);
            var expected = 10;

            // Assert
            Assert.Equal(expected, resultado.TotalPages);
        }

        [Theory]
        [InlineData("", 10, 10)]
        [InlineData("Cliente", 4, 25)]
        [InlineData("", 100, 1)]
        [InlineData("Tony Stark", 100, 0)]
        public async Task TestCustomerPaginationDynamicallyTest(string filter, int rows, int expected)
        {
            // Arrange

            var mapper = new Mock<IMapper>();
            var repository = new CustomerRepository(Context, mapper.Object);
            var logger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repository, logger.Object, mapper.Object);

            // Act

            var actual = await service.GetCollectionAsync(filter, 1, rows);

            // Assert
            Assert.Equal(expected, actual.TotalPages);
        }
    }
}
