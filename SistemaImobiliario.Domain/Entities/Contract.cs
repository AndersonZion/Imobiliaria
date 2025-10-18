


using SistemaImobiliario.Shared.Notifications;

namespace SistemaImobiliario.Domain.Entities
{
    public class Contract : EntityBase
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal ValorAluguel { get; set; }
        public bool Status { get; set; }
        public int TenantId { get; set; } // Chave estrangeira
        public Tenant Tenant { get; set; } = null!;

        public Contract()
        {

        }
        public Contract(DateTime dataInicio, DateTime dataFim, decimal valorAluguel, bool status, int tenantId, Tenant tenant)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            ValorAluguel = valorAluguel;
            Status = status;
            TenantId = tenantId;
            Tenant = tenant;
        }


    }
}