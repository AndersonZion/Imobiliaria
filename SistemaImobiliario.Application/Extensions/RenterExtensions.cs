using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Domain.Entities;

namespace SistemaImobiliario.Application.Extensions
{
    public static class RenterExtensions
    {
        public static RenterDto ToDto(this Renter renter)
            => new(renter); // => new RenterDto(renter);

        public static Renter ToEntity(this RenterDto dto)
            => new(dto.Name, dto.Document, dto.Email, dto.Phone); 
        //=> new Renter(dto.Name, dto.Document, dto.Email, dto.Phone);
    }
}
