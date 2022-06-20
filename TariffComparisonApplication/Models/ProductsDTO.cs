using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TariffComparisonApplication.Models
{
    public class ProductsDTO
    {
        [Required]
        [JsonRequired]
        public string TariffName { get; set; }

        [Required]
        [JsonRequired]
        public decimal AnnualCosts { get; set; }
    }
}
