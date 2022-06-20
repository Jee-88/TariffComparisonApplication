using System.ComponentModel.DataAnnotations;
using TariffComparisonApplication.Models.Enum;

namespace TariffComparisonApplication.Models.Entity
{
    public class ProductEntity
    {
        public Guid ProductEntityId { get; set; }

        public ProductType productType { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public ProductCostEntity ProductCost { get; set; }
    }
}
