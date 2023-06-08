using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class CategoryReadContext : DbContext
    {
        public CategoryReadContext(DbContextOptions<CategoryReadContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}


