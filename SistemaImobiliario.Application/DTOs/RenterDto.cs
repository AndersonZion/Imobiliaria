using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Application.DTOs
{
    public class RenterDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public RenterDto() { }

        public RenterDto(Renter renter)
        {
            Id = renter.Id;
            Name = renter.Name.Name;       // Extrai Value do NameVO
            Document = renter.Document.Document; // Extrai Value do DocumentVO
            Email = renter.Email.Email;     // Extrai Value do EmailVO
                                            // Phone = renter.Phone.Phone;     // Extrai Value do PhoneVO
            Phone = renter.Phone.ToString(); //ao mapear para o DTO.
        }
    }
}
