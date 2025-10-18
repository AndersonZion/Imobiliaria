
using SistemaImobiliario.Shared.Notifications;
using SistemaImobiliario.Domain.ValueObjects;

namespace SistemaImobiliario.Domain.Entities
{
    public class Tenant : EntityBase
    {
        public NameVO Name { get; private set; } = null!;
        public DocumentVO Document { get; private set; } = null!;
        public EmailVO Email { get; private set; } = null!;
        public PhoneVO Phone { get; private set; } = null!;
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

        public Tenant() { } // Necessário para o EF
        public Tenant(string name, string document, string email, string phone)
        {
            ClearNotifications();  // limpa ao atualizar
            Name = new NameVO(name);
            Document = new DocumentVO(document);
            Email = new EmailVO(email);
            Phone = new PhoneVO(phone);
            Validate();
        }

        private void Validate()
        {
            // ClearNotifications();

            AddNotifications(Name.Notifications);
            AddNotifications(Phone.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Document.Notifications);
        }

        public void Update(int id, string name, string document, string email, string phone)
        {
            ClearNotifications();  // limpa ao atualizar
            Id = id;
            Name = new NameVO(name);
            Document = new DocumentVO(document);
            Email = new EmailVO(email);
            Phone = new PhoneVO(phone);

            Validate();
        }

        // public void ValidateDuplicateDocument(bool alreadyExists)
        // {
        //     if (alreadyExists)
        //         AddNotification(nameof(Document), "Documento já cadastrado.");
        // }

        public void ValidateIf(bool condition, string propertyName, string errorMessage)
        {
            if (condition)
                AddNotification(propertyName, errorMessage);
        }

    }
}