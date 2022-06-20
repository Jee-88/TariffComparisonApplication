using TariffComparisonApplication.Models;
using TariffComparisonApplication.Models.Entity;
using TariffComparisonApplication.Models.Enum;

namespace TariffComparisonApplication.HandlerService
{
    public class ProductDataHandler : IProductDataHandler
    {
        private readonly ProductDataBaseContext _context;

        public ProductDataHandler(ProductDataBaseContext context)
        {
            _context = context;
        }
        public async Task SetUpProductData()
        {
            if(_context.Products.Any())
            {
                _context.RemoveRange(_context.Products);
            }

            List<ProductEntity> productEntities = new List<ProductEntity>();
            var pID = Guid.NewGuid();
            productEntities.Add(new ProductEntity
            {
                ProductName = "Basic Electricity Tariff",
                productType = ProductType.BasicElectricityTariff,
                ProductEntityId = pID,
                ProductDescription = "Basic plan",
                ProductCost = new ProductCostEntity
                {
                    ProductEntityId = pID,
                    ProductCostEntityId = Guid.NewGuid(),
                    AdditionalCost = 0.22m,
                    BaseCost = 5,
                    BaseLimit = 12
                }
            });
            pID = Guid.NewGuid();
            productEntities.Add(new ProductEntity
            {
                ProductName = "Packaged Tariff",
                productType = ProductType.PackagedTariff,
                ProductEntityId = pID,
                ProductDescription = "Package plan",
                ProductCost = new ProductCostEntity
                {
                    ProductEntityId = pID,
                    ProductCostEntityId = Guid.NewGuid(),
                    PackageFixedCost = 800,
                    PackageLimit = 4000,
                    AdditionalCost = 0.30m,
                    BaseCost = 10,
                    BaseLimit = 10
                }
            });
            _context.Products.AddRange(productEntities);
            await _context.SaveChangesAsync();
        }
    }
}
