
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Application.DTOs
{
    public class TenantDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public TenantDto() { }

        public TenantDto(Tenant tenant)
        {
            Id = tenant.Id;
            Name = tenant.Name.Name;
            Document = tenant.Document.Document;
            Email = tenant.Email.Email;
            Phone = tenant.Phone.Phone;
        }
    }
}