using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TariffComparisonApplication.Models
{
    public class ConsumptionDTO
    {
        public ConsumptionDTO()
        { }

        [Required]
        [JsonRequired]
        public int Consumption { get; set; }

    }
}
