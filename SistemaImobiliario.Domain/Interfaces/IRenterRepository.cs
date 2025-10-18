using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaImobiliario.Domain.Interfaces
{
    public interface IRenterRepository
    {
        Task AddAsync(Renter renter);
        Task Update(Renter renter);
        void Delete(Renter renter);
        Task<Renter?> GetByIdAsync(int id);
        Task<IEnumerable<Renter>> GetAllAsync();
        Task<(IEnumerable<Renter> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? search = null, string? orderBy = null, bool ascending = true);
        Task<bool> ExistsByDocumentAsync(string document, int? ignoreId = null);
        Task<bool> ExistsByEmailAsync(string email, int? ignoreId = null);
        Task<bool> ExistsByPhoneAsync(string phone, int? ignoreId = null);
        Task SaveChangesAsync();

        Task<(IEnumerable<Renter> Items, int TotalCount)> GetPagedAsync(PaginationFilter filter);
    }
}
