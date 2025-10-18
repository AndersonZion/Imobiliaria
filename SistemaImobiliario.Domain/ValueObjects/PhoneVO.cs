using System.Text.RegularExpressions;
using SistemaImobiliario.Shared.Notifications;

namespace SistemaImobiliario.Domain.ValueObjects
{
    public class PhoneVO : Notifiable
    {
        public string Phone { get; }

        public PhoneVO(string phone)
        {
            Phone = phone;
            Validate();
        }

        private void Validate()
        {
            ClearNotifications();


            if (!ValidationExtensions.ValidateRequired(
                 Phone,
                 nameof(Phone),
                 "Phone é obrigatório.",
                 n => AddNotification(n.Property, n.Message)))
                return;

            if (!ValidationExtensions.ValidateRegex(
                 Phone,
                        @"^(?:[1-9][1-9])(?:9\d{8}|\d{8})$", // validar celulares e fixos sem +55
                        nameof(Phone),
                        "Formato do Phone é inválido",
                        n => AddNotification(n.Property, n.Message)))
                return;





            // // Exemplo de regex para telefone com DDD ou internacional
            // var regex = new Regex(@"^\+?[1-9]\d{7,14}$");
            // if (!regex.IsMatch(Phone))
            // {
            //     AddNotification(nameof(Phone), "Formato do Phone é inválido.");
            // }
        }

        public override string ToString() => Phone;
    }
}
