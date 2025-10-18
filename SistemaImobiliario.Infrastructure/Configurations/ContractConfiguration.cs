using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Infrastructure.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");

            builder.HasKey(c => c.Id);
            // builder.Property(c => c.Id).HasColumnName("IdContrato");

            builder.Property(c => c.DataInicio)
                .IsRequired();

            builder.Property(c => c.DataFim)
                .IsRequired();

            builder.Property(c => c.ValorAluguel)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Status)
                .IsRequired();

            // Relacionamento: Contract tem 1 Tenant, Tenant tem muitos Contracts
            builder.HasOne(c => c.Tenant)
                .WithMany(t => t.Contracts)
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}