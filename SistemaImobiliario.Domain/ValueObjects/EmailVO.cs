using System.Text.RegularExpressions;
using SistemaImobiliario.Domain.Notifications;

namespace SistemaImobiliario.Domain.ValueObjects
{
    public class EmailVO : Notifiable
    {
        public string Email { get; }

        public EmailVO(string email)
        {
            Email = email;
            Validate();
        }

        private void Validate()
        {
            ClearNotifications();

            if (!ValidationExtensions.ValidateRequired(
            Email,
            nameof(Email),
            "Emaill é obrigatório.",
            n => AddNotification(n.Property, n.Message)))
                return;

            // Exemplo de regex para e-mail
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!regex.IsMatch(Email))
            {
                AddNotification(nameof(Email), "Formato do Emaill é inválido.");
            }
        }

        public override string ToString() => Email;
    }
}