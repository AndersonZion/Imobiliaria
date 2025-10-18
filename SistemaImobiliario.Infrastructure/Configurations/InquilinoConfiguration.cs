using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Infrastructure.Configurations
{
    public class InquilinoConfiguration : IEntityTypeConfiguration<Inquiline>
    {
        public void Configure(EntityTypeBuilder<Inquiline> builder)
        {
            builder.ToTable("Inquilinos");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Document).IsRequired().HasMaxLength(20);
            builder.Property(i => i.Email).HasMaxLength(100);
            builder.Property(i => i.Phone).HasMaxLength(20);
        }
    }
}