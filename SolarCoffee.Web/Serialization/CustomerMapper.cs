using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class CustomerMapper
    {
        public static CustomerViewModel SerializeCustomer(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        public static CustomerAddressViewModel MapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressViewModel
            {

            };
        }

        public static CustomerAddress MapCustomerAddress(CustomerAddressViewModel address)
        {
            return new CustomerAddress
            {

            };
        }
    }
}
