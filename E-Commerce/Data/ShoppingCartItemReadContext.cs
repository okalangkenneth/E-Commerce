using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class ShoppingCartItemReadContext : DbContext
    {
        public ShoppingCartItemReadContext(DbContextOptions<ShoppingCartItemReadContext> options) : base(options)
        {
        }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartItem>().ToView("View_ShoppingCartItems");
            base.OnModelCreating(modelBuilder);
        }
    }

}
