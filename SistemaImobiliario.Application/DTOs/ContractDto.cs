using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaImobiliario.Application.DTOs
{
    public class ContractDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal ValorAluguel { get; set; }
        public bool Status { get; set; }
        public int TenantId { get; set; }
    }
}