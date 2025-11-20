using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nexus.Catalog.API.Controllers;
using Nexus.Catalog.API.DTOs;
using Nexus.Catalog.API.Entities;
using Nexus.Catalog.API.Repositories;
using Xunit;

namespace Nexus.Catalog.API.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ProductsController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk_WhenProductsExist()
        {
            var fakeProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1000 },
                new Product { Id = 2, Name = "Mouse", Price = 20 }
            };

            var fakeDtos = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Laptop", Price = 1000 },
                new ProductDto { Id = 2, Name = "Mouse", Price = 20 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(fakeProducts);

            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(fakeProducts))
                .Returns(fakeDtos);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnProducts = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);

            Assert.Equal(2, returnProducts.Count());
        }
    }
}