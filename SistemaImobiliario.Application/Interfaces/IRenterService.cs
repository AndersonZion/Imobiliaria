using SistemaImobiliario.Application.Common;
using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaImobiliario.Application.Interfaces
{
    public interface IRenterService
    {
        Task<ServiceResult<RenterDto>> CreateAsync(RenterDto dto);
        Task<ServiceResult<RenterDto>> UpdateAsync(int id, RenterDto dto);
        Task<ServiceResult<RenterDto?>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<RenterDto>>> GetAllAsync();
        Task<ServiceResult<bool>> DeleteAsync(int id);
        Task<ServiceResult<PagedResultDto<RenterDto>>> GetPagedAsync(int pageNumber = 1, int pageSize = 10, string? search = null, string? orderBy = null, bool ascending = true);
        Task<ServiceResult<PagedResultDto<RenterDto>>> GetPagedAsync(PaginationFilter filter);
    }
}
