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

        public DbSet<Car> Car { get; set; }
    }
}
