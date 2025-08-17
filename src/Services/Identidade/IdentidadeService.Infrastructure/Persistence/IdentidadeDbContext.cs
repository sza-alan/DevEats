using IdentidadeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentidadeService.Infrastructure.Persistence
{
    public class IdentidadeDbContext : DbContext
    {
        public IdentidadeDbContext(DbContextOptions<IdentidadeDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
