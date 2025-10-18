
using Microsoft.EntityFrameworkCore;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Infrastructure.Data;

namespace SistemaImobiliario.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tenant tenant)
        {
            await _context.Tenants.AddAsync(tenant);
        }

        public void Delete(Tenant tenant)
        {
            _context.Tenants.Remove(tenant);
        }


        public async Task<bool> ExistsByDocumentAsync(string document, int? ignoreId = null)
        {
            return await _context.Tenants
                  .AnyAsync(x => x.Document
                  .Document == document && (!ignoreId.HasValue || x.Id != ignoreId.Value));
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? ignoreId = null)
        {
            return await _context.Tenants
                 .AnyAsync(x => x.Email.Email == email && (!ignoreId.HasValue || x.Id != ignoreId.Value));
        }

        public async Task<bool> ExistsByPhoneAsync(string phone, int? ignoreId = null)
        {
            return await _context.Tenants
                .AnyAsync(x => x.Phone.Phone == phone && (!ignoreId.HasValue || x.Id != ignoreId.Value));
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync()
        {
            return await _context.Tenants.AsNoTracking().ToListAsync();
        }

        public async Task<Tenant?> GetByIdAsync(int id)
        {
            return await _context.Tenants.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

        public void Update(Tenant tenant)
        {
            _context.Tenants.Update(tenant);
        }
    }
}