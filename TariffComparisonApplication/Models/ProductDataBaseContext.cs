using Microsoft.EntityFrameworkCore;
using TariffComparisonApplication.Models.Entity;

namespace TariffComparisonApplication.Models
{
    public class ProductDataBaseContext :DbContext
    {
        public ProductDataBaseContext(DbContextOptions<ProductDataBaseContext> options)
            : base(options) { }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ProductCostEntity> ProductCosts { get; set; }
    }
}
