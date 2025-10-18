using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaImobiliario.Infrastructure.Repositories
{
    public class InquilineRepository : IInquilineRepository
    {
        private readonly AppDbContext _context;

        public InquilineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Inquiline inquiline)
        {
            await _context.Inquilines.AddAsync(inquiline);
        }

        public void Delete(Inquiline inquiline)
        {
            _context.Inquilines.Remove(inquiline);
        }

        public async Task<bool> ExistsByDocumentAsync(string document)
        {
            return await _context.Inquilines.AnyAsync(x => x.Document == document);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Inquilines.AnyAsync(x => x.Email == email);
        }


        public async Task<bool> ExistsByPhoneAsync(string phone, int? ignoreId = null)
        {
            return await _context.Inquilines
                .AnyAsync(x => x.Phone == phone && (!ignoreId.HasValue || x.Id != ignoreId.Value));
        }

        public async Task<IEnumerable<Inquiline>> GetAllAsync()
        {
            return await _context.Inquilines.AsNoTracking().ToListAsync();
        }

        public async Task<Inquiline?> GetByIdAsync(int id)
        {
            return await _context.Inquilines.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Inquiline inquiline)
        {
            _context.Inquilines.Update(inquiline);
        }
    }
}
