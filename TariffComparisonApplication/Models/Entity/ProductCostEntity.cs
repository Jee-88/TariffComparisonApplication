namespace TariffComparisonApplication.Models.Entity
{
    public class ProductCostEntity
    {
        public Guid ProductEntityId { get; set; }

        public Guid ProductCostEntityId { get; set; }

        public long? PackageLimit { get; set; }

        public decimal? PackageFixedCost { get; set; }

        public int BaseLimit { get; set; }

        public decimal BaseCost { get; set; }

        public decimal AdditionalCost { get; set; }
    }
}
