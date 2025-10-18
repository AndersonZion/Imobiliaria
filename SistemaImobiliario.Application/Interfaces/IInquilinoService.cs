using SistemaImobiliario.Shared.Common;
using SistemaImobiliario.Application.DTOs;

namespace SistemaImobiliario.Application.Interfaces
{
    public interface IInquilinoService
    {
        Task<ServiceResult<IEnumerable<InquilineDto>>> GetAllAsync();
        Task<ServiceResult<InquilineDto?>> GetByIdAsync(int id);
        Task<ServiceResult<InquilineDto>> CreateAsync(InquilineDto dto);
        Task<ServiceResult<InquilineDto>> UpdateAsync(int id, InquilineDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}