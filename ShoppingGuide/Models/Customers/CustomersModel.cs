using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingGuide.DbModels
{
    public class Customers
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronimic { get; set; }
        //public CustomersAdditional Additional { get; set; }
    }
}
