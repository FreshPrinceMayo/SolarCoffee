using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/api/invoice")]
        public IActionResult GenerateNewOrder([FromBody] InvoiceViewModel invoice)
        {
            _logger.LogInformation("Generaring Invoice");
            var order = OrderMapper.SeralizeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok("");
        }

        [HttpPost("/api/order")]
        public IActionResult GetOrders()
        {
            _logger.LogInformation("Get Orders");
            var orders = _orderService.GetOrders();
            var orderViewModels = OrderMapper.SeralizeOrdersViewModels(orders);
            return Ok(orderViewModels);
        }


        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            var response = _orderService.MarkFulfilled(id);
            return Ok(response);
        }
    }
}
