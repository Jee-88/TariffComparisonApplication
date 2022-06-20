using Microsoft.AspNetCore.Mvc;
using TariffComparisonApplication.HandlerService;
using TariffComparisonApplication.Models;

namespace TariffComparisonApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffComparisonController : ControllerBase
    {
        private readonly ILogger<TariffComparisonController> _logger;
        private readonly IProductCostComparisonHandler _handler;

        public TariffComparisonController(ILogger<TariffComparisonController> logger, IProductCostComparisonHandler handler)
        {
            _logger = logger;
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        [ProducesResponseType(StatusCodes.Status200OK,Type= typeof(List<ProductsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CostComparison")]
        [HttpGet]
        public async Task<List<ProductsDTO>> GetProductCostsList([FromQuery]ConsumptionDTO request)
        {
            try
            {
                return await _handler.GetProductCostsList(request);
            }
            catch(Exception ex)
            {
                _logger.LogError("Product tariff comparison failed: " + ex);
                throw new ArgumentException();
            }
        }
    }
}