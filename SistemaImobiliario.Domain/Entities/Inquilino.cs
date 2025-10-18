using SistemaImobiliario.Domain.Notifications;
using System.Text.RegularExpressions;

namespace SistemaImobiliario.Domain.Entities
{
    public class Inquiline : EntityBase
    {

        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        private List<Notification> _notifications = [];
        public new IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        public new bool IsValid => _notifications.Count == 0;

        public Inquiline(string name, string document, string email, string phone)
        {
            Name = name;
            Document = document;
            Email = email;
            Phone = phone;

            Validate();
        }

        private void Validate()
        {
            _notifications.Clear();

            if (string.IsNullOrWhiteSpace(Name))
                _notifications.Add(new Notification(nameof(Name), "Nome � obrigat�rio"));

            if (string.IsNullOrWhiteSpace(Document))
                _notifications.Add(new Notification(nameof(Document), "Documento � obrigat�rio"));

            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^\S+@\S+\.\S+$"))
                _notifications.Add(new Notification(nameof(Email), "Email inv�lido"));

            if (string.IsNullOrWhiteSpace(Phone))
                _notifications.Add(new Notification(nameof(Phone), "Telefone � obrigat�rio"));
        }

        public new void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }

        // M�todo para atualizar dados mantendo valida��o
        public void Update(string name, string document, string email, string phone)
        {
            Name = name;
            Document = document;
            Email = email;
            Phone = phone;

            Validate();
        }
    }
}