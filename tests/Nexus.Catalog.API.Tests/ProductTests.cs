using Nexus.Catalog.API.Entities;
using Xunit;

namespace Nexus.Catalog.API.Tests
{
    public class ProductTests
    {
        [Fact]
        public void RemoveStock_ShouldReduceStock_WhenQuantityIsValid()
        {
            var product = new Product
            {
                Name = "Test Product",
                Price = 100,
                Stock = 10
            };

            var quantityToRemove = 3;

            product.RemoveStock(quantityToRemove);

            Assert.Equal(7, product.Stock);
        }

        [Fact]
        public void RemoveStock_ShouldThrowException_WhenNotEnoughStock()
        {
            var product = new Product
            {
                Name = "Test Product",
                Stock = 5
            };

            var quantityToRemove = 10;

            var exception = Assert.Throws<InvalidOperationException>(() => 
            {
                product.RemoveStock(quantityToRemove);
            });

            Assert.Equal("Not enough stock available.", exception.Message);
        }
    }
}