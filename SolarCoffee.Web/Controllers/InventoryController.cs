using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet("/api/inventory")]
        public IActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventory");
            var inventory = _inventoryService.GetCurrentInventory();
            var inventoryViewModels = InventoryMapper.SerializeInventory(inventory);
            return Ok(inventoryViewModels);
        }

        [HttpPatch("/api/inventory")]
        public IActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            _logger.LogInformation($"Updating product : {shipment.ProductId} by quantity {shipment.Adjustment}");

            var response = _inventoryService.UpdateUnitsAvailable(shipment.ProductId, shipment.Adjustment);
            return Ok(response);
        }
    }
}
