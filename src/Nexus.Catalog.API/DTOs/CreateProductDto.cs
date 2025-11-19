using System.ComponentModel.DataAnnotations;

namespace Nexus.Catalog.API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}