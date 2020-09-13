using System;
using System.Collections.Generic;

namespace SolarCoffee.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}
