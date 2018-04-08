using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingGuide.DbModels
{
    public class Shopping
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DatePurchase { get; set; }
        public int SumReciept { get; set; }
        [NotMapped,]
        public PhotoModel Photo { get; set; }
    }

    public class PhotoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
