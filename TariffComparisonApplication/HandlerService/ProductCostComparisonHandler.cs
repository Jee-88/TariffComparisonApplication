using TariffComparisonApplication.Models;
using TariffComparisonApplication.Models.Entity;
using TariffComparisonApplication.Models.Enum;

namespace TariffComparisonApplication.HandlerService
{
    public class ProductCostComparisonHandler : IProductCostComparisonHandler
    {
        private readonly ProductDataBaseContext _context;
        private readonly IProductDataHandler _dataHandler;

        public ProductCostComparisonHandler(ProductDataBaseContext context, IProductDataHandler dataHandler)
        {
            _context = context;
            _dataHandler = dataHandler;
        }

        public decimal CalculateProductAnnualCost(ProductType productType, ProductCostEntity productCost, int consumption)
        {
            switch (productType)
            {
                case ProductType.BasicElectricityTariff:
                    return (productCost.BaseCost * productCost.BaseLimit) + (consumption * productCost.AdditionalCost);

                case ProductType.PackagedTariff:
                    var tariffLimitDiff = consumption - productCost.PackageLimit;
                    if (tariffLimitDiff < 0)
                        return productCost.PackageFixedCost ?? 0;
                    else
                        return (decimal)((productCost.PackageFixedCost ?? 0) + (tariffLimitDiff * productCost.AdditionalCost));
                
                default: return 0;
            }
        }

        public async Task<List<ProductsDTO>> GetProductCostsList(ConsumptionDTO consumption)
        {
            await _dataHandler.SetUpProductData();
            List<ProductsDTO> responseDto = new List<ProductsDTO>();
            foreach(var product in _context.Products)
            {
                responseDto.Add(new ProductsDTO()
                {
                    TariffName = product.ProductName,
                    AnnualCosts = CalculateProductAnnualCost(product.productType, product.ProductCost, consumption.Consumption)
                });
            }
            return responseDto.OrderBy(x => x.AnnualCosts).ToList();
        }

    }
}
