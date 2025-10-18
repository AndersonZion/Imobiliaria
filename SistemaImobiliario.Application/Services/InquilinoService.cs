using SistemaImobiliario.Shared.Common;
using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Application.Interfaces;
using SistemaImobiliario.Domain.Entities;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Shared.Notifications;

namespace SistemaImobiliario.Application.Services
{
    public class InquilinoService : IInquilinoService
    {
        private readonly IInquilineRepository _repository;

        public InquilinoService(IInquilineRepository repository)
        {
            _repository = repository;
        }

        async Task<ServiceResult<InquilineDto>> IInquilinoService.CreateAsync(InquilineDto dto)
        {
            var inquilino = new Inquiline(dto.Nome, dto.Documento, dto.Email, dto.Telefone);

            if (!inquilino.IsValid)
                return ServiceResult<InquilineDto>.Fail(inquilino.Notifications);

            //if (!inquilino.IsValid)
            //    return ServiceResult<InquilineDto>
            //        .Fail((IEnumerable<Notification>)inquilino.Notifications.Select(n => n.Message).ToList());

            if (await _repository.ExistsByDocumentAsync(dto.Documento))
                inquilino.AddNotification(nameof(inquilino.Document), "Documento j� cadastrado.");

            if (await _repository.ExistsByEmailAsync(dto.Email))
                inquilino.AddNotification(nameof(inquilino.Email), "Email j� cadastrado.");

            if (await _repository.ExistsByPhoneAsync(dto.Telefone))
                inquilino.AddNotification(nameof(inquilino.Phone), "Telefone ja cadastrado.");

            if (!inquilino.IsValid)
                return ServiceResult<InquilineDto>.Fail(inquilino.Notifications);

            await _repository.AddAsync(inquilino);
            await _repository.SaveChangesAsync();

            return ServiceResult<InquilineDto>.Ok(new InquilineDto(inquilino));


        }

        async Task<ServiceResult<bool>> IInquilinoService.DeleteAsync(int id)
        {
            var inquilino = await _repository.GetByIdAsync(id);
            if (inquilino == null)
                return ServiceResult<bool>.Fail("Id", "Inquilino n�o encontrado");

            _repository.Delete(inquilino);
            await _repository.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true);
        }

        async Task<ServiceResult<IEnumerable<InquilineDto>>> IInquilinoService.GetAllAsync()
        {
            var inquilinos = await _repository.GetAllAsync();
            var dtos = inquilinos.Select(i => new InquilineDto(i));
            return ServiceResult<IEnumerable<InquilineDto>>.Ok(dtos);
        }

        async Task<ServiceResult<InquilineDto?>> IInquilinoService.GetByIdAsync(int id)
        {
            var inquilino = await _repository.GetByIdAsync(id);
            if (inquilino == null)
                return ServiceResult<InquilineDto?>.Fail(new[] { new Notification("Id", "Inquilino n�o encontrado") });

            return ServiceResult<InquilineDto?>.Ok(new InquilineDto(inquilino));
        }

        async Task<ServiceResult<InquilineDto>> IInquilinoService.UpdateAsync(int id, InquilineDto dto)
        {
            var inquilino = await _repository.GetByIdAsync(id);
            if (inquilino == null)
                return ServiceResult<InquilineDto>.Fail("Id", "Inquilino n�o encontrado");

            inquilino.Update(dto.Nome, dto.Documento, dto.Email, dto.Telefone);

            if (!inquilino.IsValid)
                return ServiceResult<InquilineDto>.Fail(inquilino.Notifications);

            // Verifica��es para campos �nicos ignorando o pr�prio registro
            if (await _repository.ExistsByDocumentAsync(dto.Documento) && dto.Documento != inquilino.Document)
                inquilino.AddNotification(nameof(inquilino.Document), "Documento j� cadastrado.");

            if (await _repository.ExistsByEmailAsync(dto.Email) && dto.Email != inquilino.Email)
                inquilino.AddNotification(nameof(inquilino.Email), "Email j� cadastrado.");

            if (await _repository.ExistsByPhoneAsync(dto.Telefone) && dto.Telefone != inquilino.Phone)
                inquilino.AddNotification(nameof(inquilino.Phone), "Telefone j� cadastrado.");

            if (!inquilino.IsValid)
                return ServiceResult<InquilineDto>.Fail(inquilino.Notifications);

            _repository.Update(inquilino);
            await _repository.SaveChangesAsync();

            return ServiceResult<InquilineDto>.Ok(new InquilineDto(inquilino));
        }
    }
}
