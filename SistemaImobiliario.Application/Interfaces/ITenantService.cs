
using SistemaImobiliario.Shared.Common;
using SistemaImobiliario.Application.DTOs;

namespace SistemaImobiliario.Application.Interfaces
{
    public interface ITenantService
    {

        Task<ServiceResult<IEnumerable<TenantDto>>> GetAllAsync();
        Task<ServiceResult<TenantDto?>> GetByIdAsync(int id);
        Task<ServiceResult<TenantDto>> CreateAsync(TenantDto dto);
        Task<ServiceResult<TenantDto>> UpdateAsync(int id, TenantDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}