using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Infrastructure.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");
            builder.HasKey(i => i.Id);
            // Configuração para Value Object: Name
            builder.OwnsOne(i => i.Name, name =>
            {
                name.Property(n => n.Name)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(100);
            });
            // builder.Property(i => i.Document).IsRequired().HasMaxLength(20);

            builder.OwnsOne(i => i.Document, document =>
       {
           document.Property(p => p.Document)
               .HasColumnName("Document")
               .HasMaxLength(20);
       });

            builder.OwnsOne(i => i.Email, email =>
          {
              email.Property(p => p.Email)
                  .HasColumnName("Email")
                  .HasMaxLength(100);
          });
            // Configuração para Value Object: Phone
            builder.OwnsOne(i => i.Phone, phone =>
            {
                phone.Property(p => p.Phone)
                    .HasColumnName("Phone")
                    .HasMaxLength(20);
            });
        }
    }
}