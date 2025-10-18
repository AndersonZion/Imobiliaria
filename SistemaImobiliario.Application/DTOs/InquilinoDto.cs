using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Application.DTOs
{
    public class InquilineDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public InquilineDto() { }

        public InquilineDto(Inquiline inquilino)
        {
            Id = inquilino.Id;
            Nome = inquilino.Name;
            Documento = inquilino.Document;
            Email = inquilino.Email;
            Telefone = inquilino.Phone;
        }




    }
}