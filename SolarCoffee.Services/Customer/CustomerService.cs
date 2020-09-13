using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;
        public CustomerService(SolarDbContext dbcontext)
        {
            _db = dbcontext;
        }
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                customer.CreatedDate = DateTime.Now;
                customer.UpdatedDate = DateTime.Now;
                customer.PrimaryAddress.CreatedDate = DateTime.Now;
                customer.PrimaryAddress.UpdatedDate = DateTime.Now;

                _db.Customers.Add(customer);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    IsSuccess = true,
                    Message = "created Successfully",
                    Time = DateTime.UtcNow
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;


            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    Data = false,
                    IsSuccess = false,
                    Message = "Can't find customer"
                };
            }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Time = now,
                    Data = true,
                    IsSuccess = true,
                    Message = "Customer deleted"
                };
            }
            catch (Exception)
            {

                return new ServiceResponse<bool>
                {
                    Time = now,
                    Data = false,
                    IsSuccess = false,
                    Message = "Error saving data"
                };
            }

        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(x => x.PrimaryAddress)
                .OrderBy(y => y.LastName)
                .ToList();
        }

        public Data.Models.Customer GetById(int id)
        {
            var customer = _db.Customers.Find(id);
            return customer;
        }
    }
}
