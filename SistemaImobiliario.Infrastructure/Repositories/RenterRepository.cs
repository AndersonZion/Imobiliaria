using Microsoft.EntityFrameworkCore;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Infrastructure.Data;
using SistemaImobiliario.Shared.Filters;

namespace SistemaImobiliario.Infra.Repositories
{
    public class RenterRepository : IRenterRepository
    {
        private readonly AppDbContext _context;

        public RenterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Renter renter) => await _context.Renters.AddAsync(renter);

        public void Delete(Renter renter) => _context.Renters.Remove(renter);

        public async Task<IEnumerable<Renter>> GetAllAsync() =>
            await _context.Renters.AsNoTracking().ToListAsync();

        public async Task<Renter?> GetByIdAsync(int id) =>
            await _context.Renters.FindAsync(id);

        public async Task Update(Renter renter)
        {
            _context.Renters.Update(renter);
            await Task.CompletedTask;
        }


        //acessando a string interna de cada VO  x.Document.Document
        public async Task<bool> ExistsByDocumentAsync(string document, int? ignoreId = null) =>
            await _context.Renters
            .AnyAsync(x => x.Document.Document == document && (!ignoreId.HasValue || x.Id != ignoreId.Value));

        public async Task<bool> ExistsByEmailAsync(string email, int? ignoreId = null) =>
            await _context.Renters
            .AnyAsync(x => x.Email.Email == email && (!ignoreId.HasValue || x.Id != ignoreId.Value));

        public async Task<bool> ExistsByPhoneAsync(string phone, int? ignoreId = null) =>
            await _context.Renters
            .AnyAsync(x => x.Phone.Phone == phone && (!ignoreId.HasValue || x.Id != ignoreId.Value));

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();


        // Paginação: usa AsNoTracking e projeta entidades
        public async Task<(IEnumerable<Renter> Items, int TotalCount)> GetPagedAsync(
           int pageNumber,
           int pageSize,
           string? search = null,
           string? orderBy = null,
           bool ascending = true)
        {
            var query = _context.Renters.AsNoTracking();

            // Filtro
            if (!string.IsNullOrWhiteSpace(search))
            {

                query = query.Where(r => EF.Functions.Like(Convert.ToString(r.Name), $"%{search}%"));
            }

            var total = await query.CountAsync();

            

            if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                query = ascending
                    ? query.OrderBy(r => r.Name)
                    : query.OrderByDescending(r => r.Name);
            }

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

      
        public async Task<(IEnumerable<Renter> Items, int TotalCount)> GetPagedAsync(PaginationFilter filter)
        {
            var query = _context.Renters.AsNoTracking();

            // Filtro por nome
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(r => EF.Functions.Like(Convert.ToString(r.Name), $"%{filter.Search}%"));
            }

            // Ordenação
            if (string.IsNullOrWhiteSpace(filter.OrderBy) || filter.OrderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                query = filter.Ascending
                    ? query.OrderBy(r => r.Name)
                    : query.OrderByDescending(r => r.Name);
            }

            var total = await query.CountAsync();

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (items, total);
        }
    } 
}


