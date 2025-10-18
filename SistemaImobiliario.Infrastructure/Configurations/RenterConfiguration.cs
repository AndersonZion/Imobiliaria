


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.ValueObjects;

namespace SistemaImobiliario.Infra.Configurations
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder.ToTable("Renters");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                     .HasConversion(vo => vo.Name, str => new NameVO(str))
                     .HasMaxLength(100)
                     .IsRequired();

            builder.Property(r => r.Document)
                   .HasConversion(vo => vo.Document, str => new DocumentVO(str))
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(r => r.Email)
                   .HasConversion(vo => vo.Email, str => new EmailVO(str))
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(r => r.Phone)
                   .HasConversion(vo => vo.Phone, str => new PhoneVO(str))
                   .HasMaxLength(20)
                   .IsRequired();


            // Índices únicos
            builder.HasIndex("Document").IsUnique();
            builder.HasIndex("Email").IsUnique();
            builder.HasIndex("Phone").IsUnique();
        }
    }
}
