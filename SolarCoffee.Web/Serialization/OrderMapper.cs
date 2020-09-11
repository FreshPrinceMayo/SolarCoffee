using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Web.Serialization
{
    public static class OrderMapper
    {
        public static SalesOrder SeralizeInvoiceToOrder(InvoiceViewModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(x => new SalesOrderItem
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    Product = ProductMapper.SerializeProductModel(x.Product)
                }).ToList();

            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

        }
        public static List<OrderViewModel> SeralizeOrdersViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                SalesOrderItems = SerializeSalesOrderItems(x.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(x.Customer),
                IsPaid = false
            }).ToList();
        }


        public static List<SalesOrderItemViewModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> salesOrderItems)
        {
            return salesOrderItems.Select(x => new SalesOrderItemViewModel
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Product = ProductMapper.SerializeProductModel(x.Product)

            }).ToList();
        }

    }
}
