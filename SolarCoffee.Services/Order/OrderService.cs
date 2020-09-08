using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;

        private readonly IInventoryService _inventoryService;

        private readonly IProductService _productService;
        public OrderService(SolarDbContext databaseContext)
        {
            _db = databaseContext;
        }
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            var now = DateTime.UtcNow;

            try
            {
                foreach (var item in order.SalesOrderItems)
                {
                    item.Product = _productService.GetProductById(item.Product.Id);

                    var inventoryId = _inventoryService.GetProductById(item.Product.Id).Id;

                    _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
                }

                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Generated Invoice",
                    Time = now
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = $"Invoice generation failed {ex.InnerException.Message}",
                    Time = now
                };
            }

        }

        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(x => x.Customer)
                .ThenInclude(x => x.PrimaryAddress)
                .Include(x => x.SalesOrderItems)
                .ThenInclude(x => x.Product)
                .ToList();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;

            try
            {
                var salesOrder = _db.SalesOrders.Find(id);
                salesOrder.IsPaid = true;
                salesOrder.UpdatedTime = now;
                _db.SalesOrders.Update(salesOrder);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = $"SalesOrderId:{salesOrder.Id} - Sales order has been paid",
                    Time = now
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = $"Sales order could not be completed {ex.InnerException.Message}",
                    Time = now
                };
            }


        }
    }
}
