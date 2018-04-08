using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingGuide.DbModels
{    
    public class CustomersAdditional
    {
        [Key]
        public Guid Customerid { get; set; }
        public string Email { get; set; }
        public string PhoneFull { get; set; }
        public long Phone { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
