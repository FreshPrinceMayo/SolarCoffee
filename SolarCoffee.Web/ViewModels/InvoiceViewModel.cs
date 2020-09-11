using System;
using System.Collections.Generic;

namespace SolarCoffee.Web.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemViewModel> LineItems { get; set; }
    }

    public class SalesOrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductViewModel Product { get; set; }
    }

}
