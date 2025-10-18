
using SistemaImobiliario.Application.Common;
using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Application.Interfaces;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Domain.Notifications;
using System.Linq;

namespace SistemaImobiliario.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _repository;

        public TenantService(ITenantRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<TenantDto>> CreateAsync(TenantDto dto)
        {
            var tenant = new Tenant(dto.Name, dto.Document, dto.Email, dto.Phone);

            tenant.ValidateIf(await _repository.ExistsByDocumentAsync
            (dto.Document, tenant.Id), nameof(tenant.Document), "Documento já cadastrado.");

            tenant.ValidateIf(await _repository.ExistsByEmailAsync
            (dto.Email, tenant.Id), nameof(tenant.Email), "Email já cadastrado.");

            tenant.ValidateIf(await _repository.ExistsByPhoneAsync
            (dto.Phone, tenant.Id), nameof(tenant.Phone), "Phone já cadastrado.");

            if (!tenant.IsValid)
                return ServiceResult<TenantDto>.Fail(tenant.Notifications);


            await _repository.AddAsync(tenant);
            await _repository.SaveChangesAsync();

            return ServiceResult<TenantDto>.Ok(new TenantDto(tenant));
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var tenant = await _repository.GetByIdAsync(id);
            if (tenant == null)
                return ServiceResult<bool>.Fail("Id", "Tenant não encontrado");

            _repository.Delete(tenant);
            await _repository.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<IEnumerable<TenantDto>>> GetAllAsync()
        {
            var tenant = await _repository.GetAllAsync();
            var dtos = tenant.Select(i => new TenantDto(i));
            return ServiceResult<IEnumerable<TenantDto>>.Ok(dtos);
        }

        public async Task<ServiceResult<TenantDto?>> GetByIdAsync(int id)
        {
            var tenant = await _repository.GetByIdAsync(id);
            if (tenant == null)
                return ServiceResult<TenantDto?>.Fail(new[] { new Notification("Id", "Inquilino n�o encontrado") });




            return ServiceResult<TenantDto?>.Ok(new TenantDto(tenant));
        }

        public async Task<ServiceResult<TenantDto>> UpdateAsync(int id, TenantDto dto)
        {


            var tenant = await _repository.GetByIdAsync(id);
            if (tenant == null)
                return ServiceResult<TenantDto>.Fail("Id", "Inquilino n�o encontrado");

            tenant.Update(tenant.Id, dto.Name, dto.Document, dto.Email, dto.Phone);

            tenant.ValidateIf(await _repository.ExistsByDocumentAsync
            (dto.Document, tenant.Id), nameof(tenant.Document), "Documento já cadastrado.");

            tenant.ValidateIf(await _repository.ExistsByEmailAsync
            (dto.Email, tenant.Id), nameof(tenant.Email), "Email já cadastrado.");

            tenant.ValidateIf(await _repository.ExistsByPhoneAsync
            (dto.Phone, tenant.Id), nameof(tenant.Phone), "Phone já cadastrado.");


            if (!tenant.IsValid)
                return ServiceResult<TenantDto>.Fail(tenant.Notifications);

            _repository.Update(tenant);
            await _repository.SaveChangesAsync();

            return ServiceResult<TenantDto>.Ok(new TenantDto(tenant));
        }
    }
}