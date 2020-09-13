using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;

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

        public static Customer SerializeCustomer(CustomerViewModel customerViewModel)
        {
            return new Customer
            {
                CreatedDate = customerViewModel.CreatedDate,
                UpdatedDate = customerViewModel.UpdatedDate,
                FirstName = customerViewModel.FirstName,
                LastName = customerViewModel.LastName,
                PrimaryAddress = MapCustomerAddress(customerViewModel.PrimaryAddress)
            };
        }

        public static List<CustomerViewModel> SerializeCustomers(List<Customer> customers)
        {
            var customerViewModels = new List<CustomerViewModel>();

            foreach (var customer in customers)
            {
                customerViewModels.Add(new CustomerViewModel
                {
                    Id = customer.Id,
                    CreatedDate = customer.CreatedDate,
                    UpdatedDate = customer.UpdatedDate,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
                });
            }

            return customerViewModels;
        }

        public static CustomerAddressViewModel MapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressViewModel
            {
                Id = address.Id,
                UpdatedDate = address.UpdatedDate,
                CreatedDate = address.CreatedDate,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode
            };
        }

        public static CustomerAddress MapCustomerAddress(CustomerAddressViewModel addressViewModel)
        {
            return new CustomerAddress
            {
                Id = addressViewModel.Id,
                CreatedDate = addressViewModel.CreatedDate,
                AddressLine1 = addressViewModel.AddressLine1,
                AddressLine2 = addressViewModel.AddressLine2,
                City = addressViewModel.City,
                State = addressViewModel.State,
                Country = addressViewModel.Country,
                PostalCode = addressViewModel.PostalCode
            };
        }


    }
}
