using Microsoft.EntityFrameworkCore;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Notifications;

namespace SistemaImobiliario.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Inquiline> Inquilines => Set<Inquiline>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<Renter> Renters { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Ignore<Notification>();
            base.OnModelCreating(modelBuilder);
        }
    }
}