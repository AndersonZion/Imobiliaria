
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Domain.Interfaces
{
    public interface ITenantRepository
    {
       
        Task<IEnumerable<Tenant>> GetAllAsync();
        Task<Tenant?> GetByIdAsync(int id);
        Task AddAsync(Tenant tenant);
        void Update(Tenant tenant);
        void Delete(Tenant tenant);
        Task<bool> ExistsByDocumentAsync(string document, int? ignoreId = null);
        Task<bool> ExistsByEmailAsync(string email, int? ignoreId = null);
        Task<bool> ExistsByPhoneAsync(string phone, int? ignoreId = null);
        Task SaveChangesAsync();
    }
}