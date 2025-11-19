using Microsoft.EntityFrameworkCore;
using Nexus.Catalog.API.Entities;

namespace Nexus.Catalog.API.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}