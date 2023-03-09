using Microsoft.EntityFrameworkCore;
using Projecto_Final.Models;

namespace Projecto_Final.Contexts
{
    public class StoreContext : DbContext
    {
        public StoreContext() { }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Order_Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ProductColor> Colors { get; set; }
        public DbSet<ProductDiscount> Discounts { get; set; }
        public DbSet<ProductFabric> Fabrics { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
    }
}
