using System;
using System.Collections.Generic;
using SistemaImobiliario.Shared.Common;
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Application.Interfaces
{
    public interface IContractService
    {
        Task<ServiceResult<Contract>> CreateAsync(DateTime dataInicio, DateTime dataFim, decimal valorAluguel, bool status, int tenantId);
        Task<Contract?> GetByIdAsync(int id);
        Task<List<Contract>> GetAllAsync();
        Task<Contract?> UpdateAsync(int id, DateTime dataInicio, DateTime dataFim, decimal valorAluguel, bool status);
        Task<bool> DeleteAsync(int id);
    }
}