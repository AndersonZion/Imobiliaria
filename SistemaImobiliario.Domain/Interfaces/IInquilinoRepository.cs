using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Domain.Interfaces
{
    public interface IInquilineRepository
    {
        Task<IEnumerable<Inquiline>> GetAllAsync();
        Task<Inquiline?> GetByIdAsync(int id);
        Task AddAsync(Inquiline inquilino);
        void Update(Inquiline inquilino);
        void Delete(Inquiline inquilino);
        Task<bool> ExistsByDocumentAsync(string documento);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByPhoneAsync(string telefone, int? ignoreId = null);
        Task SaveChangesAsync();
    }
}