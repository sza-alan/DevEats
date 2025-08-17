using DevEats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevEats.Infrastructure.Persistence
{
    public class DevEatsDbContext : DbContext
    {
        public DevEatsDbContext(DbContextOptions<DevEatsDbContext> options) : base(options) { }

        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
