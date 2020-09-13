using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerViewModel customer)
        {
            _logger.LogInformation("Creating a new customer");

            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            var customerViewModels = CustomerMapper.SerializeCustomers(customers);

            return Ok(customerViewModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var result = _customerService.DeleteCustomer(id);
            return Ok(result);
        }

    }
}
