using Microsoft.EntityFrameworkCore;

namespace ShoppingGuide.DbModels
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        { }

        public DbSet<Shopping> Shopping { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<CustomersAdditional> CustomersAdditional { get; set; }
        public DbSet<PhotoModel> PhotoModel { get; set; }
    }
}
