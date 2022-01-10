using ECommerceAPI.Services;
using Xunit;

namespace ECommerceAPI.UnitTests
{
    public class UnitTest1
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
    }
}
