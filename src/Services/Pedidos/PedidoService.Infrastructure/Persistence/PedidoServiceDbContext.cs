using Microsoft.EntityFrameworkCore;
using PedidoService.Domain.Entities;

namespace PedidoService.Infrastructure.Persistence
{
    public class PedidoServiceDbContext : DbContext
    {
        public PedidoServiceDbContext(DbContextOptions<PedidoServiceDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne()
                .HasForeignKey(pi => pi.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ValorTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PedidoItem>()
               .Property(p => p.PrecoUnitario)
               .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
