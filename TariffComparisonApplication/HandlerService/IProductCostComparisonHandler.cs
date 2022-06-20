using System.Collections.Generic;
using TariffComparisonApplication.Models;
using TariffComparisonApplication.Models.Entity;
using TariffComparisonApplication.Models.Enum;

namespace TariffComparisonApplication.HandlerService
{
    public interface IProductCostComparisonHandler
    {
        Task<List<ProductsDTO>> GetProductCostsList(ConsumptionDTO consumption);

        decimal CalculateProductAnnualCost(ProductType productType, ProductCostEntity productCost, int consumption);
    }
}
