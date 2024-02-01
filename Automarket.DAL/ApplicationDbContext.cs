using Microsoft.EntityFrameworkCore;
using Automarket.Domain.Models;

namespace Automarket.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().Property(c => c.Price).HasPrecision(18, 2);

            modelBuilder.Entity<User>().HasMany(u => u.CartItems).WithOne(ci => ci.User).HasForeignKey(ci => ci.UserId);

            modelBuilder.Entity<CartItem>().HasOne(ci => ci.Car).WithMany().HasForeignKey(ci => ci.CarId);
        }
        public DbSet<Car> Car { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<CartItem> Cart { get; set; }
    }
}
