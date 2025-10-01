using ContasPagar.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContasPagar.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ContaPagar> ContasPagar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaPagar>().ToTable("contas_pagar");

            modelBuilder.Entity<ContaPagar>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ValorOriginal)
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DataVencimento)
                    .IsRequired();

                entity.Property(e => e.DataPagamento)
                    .IsRequired();

                entity.Property(e => e.QuantidadeDiasAtraso)
                    .IsRequired();

                entity.Property(e => e.ValorCorrigido)
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentualMultaAplicado)
                    .IsRequired()
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PercentualJurosDiaAplicado)
                    .IsRequired()
                    .HasColumnType("decimal(5, 2)");
            });
        }
    }
}