using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexus.Catalog.API.DTOs;
using Nexus.Catalog.API.Entities;
using Nexus.Catalog.API.Repositories;

namespace Nexus.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            await _repository.AddAsync(product);

            var productDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var productExisting = await _repository.GetByIdAsync(id);
            if (productExisting == null) return NotFound();

            productExisting.Name = productDto.Name;
            productExisting.Description = productDto.Description;
            productExisting.Price = productDto.Price;
            productExisting.Stock = productDto.Stock;

            await _repository.UpdateAsync(productExisting);

            return NoContent();
        }
    }
}