using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class OrderReadContext : DbContext
    {
        public OrderReadContext(DbContextOptions<OrderReadContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
