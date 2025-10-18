using SistemaImobiliario.Application.Common;
using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Application.Interfaces;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Shared.Filters;
using System.Threading.Tasks;

namespace SistemaImobiliario.Application.Services
{
    public class RenterService : IRenterService
    {
        private readonly IRenterRepository _repository;

        public RenterService(IRenterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<RenterDto>> CreateAsync(RenterDto dto)
        {
            var renter = new Renter(dto.Name, dto.Document, dto.Email, dto.Phone);

            await ValidateUniqueFields(renter, dto);

            if (!renter.IsValid)
                return ServiceResult<RenterDto>.Fail(renter.Notifications);

            await _repository.AddAsync(renter);
            await _repository.SaveChangesAsync();

            return ServiceResult<RenterDto>.Ok(new RenterDto(renter));
        }

        public async Task<ServiceResult<RenterDto>> UpdateAsync(int id, RenterDto dto)
        {
            var renter = await _repository.GetByIdAsync(id);
            if (renter == null)
                return ServiceResult<RenterDto>.Fail("Id", "Renter não encontrado");

            renter.Update(dto.Name, dto.Document, dto.Email, dto.Phone);

            await ValidateUniqueFields(renter, dto);

            if (!renter.IsValid)
                return ServiceResult<RenterDto>.Fail(renter.Notifications);

            await _repository.Update(renter);
            await _repository.SaveChangesAsync();

            return ServiceResult<RenterDto>.Ok(new RenterDto(renter));
        }

        public async Task<ServiceResult<IEnumerable<RenterDto>>> GetAllAsync()
        {
            var renters = await _repository.GetAllAsync();
            var dtos = renters.Select(r => new RenterDto(r));
            return ServiceResult<IEnumerable<RenterDto>>.Ok(dtos);
        }

        public async Task<ServiceResult<RenterDto?>> GetByIdAsync(int id)
        {
            var renter = await _repository.GetByIdAsync(id);
            if (renter == null)
                return ServiceResult<RenterDto?>.Fail("Id", "Renter não encontrado");

            return ServiceResult<RenterDto?>.Ok(new RenterDto(renter));
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var renter = await _repository.GetByIdAsync(id);
            if (renter == null)
                return ServiceResult<bool>.Fail("Id", "Renter não encontrado");

            _repository.Delete(renter);
            await _repository.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true);
        }

        // Helper para validar campos únicos
        private async Task ValidateUniqueFields(Renter renter, RenterDto dto)
        {
            renter.ValidateIf(await _repository.ExistsByDocumentAsync(dto.Document, renter.Id),
                nameof(renter.Document), "Documento já cadastrado.");

            renter.ValidateIf(await _repository.ExistsByEmailAsync(dto.Email, renter.Id),
                nameof(renter.Email), "Email já cadastrado.");

            renter.ValidateIf(await _repository.ExistsByPhoneAsync(dto.Phone, renter.Id),
                nameof(renter.Phone), "Telefone já cadastrado.");
        }

        public async Task<ServiceResult<PagedResultDto<RenterDto>>> GetPagedAsync(int pageNumber = 1, int pageSize = 10, string? search = null, string? orderBy = null, bool ascending = true)
           
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return ServiceResult<PagedResultDto<RenterDto>>.Fail("Pagination", "Page number and page size must be greater than zero.");

            var (items, total) = await _repository.GetPagedAsync(pageNumber, pageSize, search, orderBy, ascending);

            var dto = new PagedResultDto<RenterDto>
            {
                Items = items.Select(r => new RenterDto(r)).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total
            };

            return ServiceResult<PagedResultDto<RenterDto>>.Ok(dto);
        }

        public async Task<ServiceResult<PagedResultDto<RenterDto>>> GetPagedAsync(PaginationFilter filter)
        {
            if (filter.PageNumber <= 0 || filter.PageSize <= 0)
                return ServiceResult<PagedResultDto<RenterDto>>.Fail("Pagination", "Page number and page size must be greater than zero.");

            var (items, total) = await _repository.GetPagedAsync(filter);

            var dto = new PagedResultDto<RenterDto>
            {
                Items = items.Select(r => new RenterDto(r)).ToList(),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = total
            };

            return ServiceResult<PagedResultDto<RenterDto>>.Ok(dto);
        }
    }
}
