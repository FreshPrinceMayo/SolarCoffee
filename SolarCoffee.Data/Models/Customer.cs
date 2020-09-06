using System;

namespace SolarCoffee.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerAddress PrimaryAddress { get; set; }
    }
}
