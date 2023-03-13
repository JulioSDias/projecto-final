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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameConsole> Consoles { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<ProductGenre> ProductGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGenre>().HasKey(k => new { k.ProductId, k.GenreId });
        }
    }
}
