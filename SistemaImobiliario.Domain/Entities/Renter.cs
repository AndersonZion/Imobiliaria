using SistemaImobiliario.Domain.Notifications;
using SistemaImobiliario.Domain.ValueObjects;

namespace SistemaImobiliario.Domain.Entities
{
    public class Renter : EntityBase
    {
        public NameVO Name { get; private set; } = null!;
        public DocumentVO Document { get; private set; } = null!;
        public EmailVO Email { get; private set; } = null!;
        public PhoneVO Phone { get; private set; } = null!;

        // Propriedades de conveniência para EF e LINQ
        public string NameStr => Name.Name;
        public string DocumentStr => Document.Document;
        public string EmailStr => Email.Email;
        public string PhoneStr => Phone.Phone;

        public Renter() { } // Para EF

        public Renter(string name, string document, string email, string phone)
        {
            ClearNotifications();

            Name = new NameVO(name);
            Document = new DocumentVO(document);
            Email = new EmailVO(email);
            Phone = new PhoneVO(phone);

            Validate();
        }

        public void Update(string name, string document, string email, string phone)
        {
            ClearNotifications();

            Name = new NameVO(name);
            Document = new DocumentVO(document);
            Email = new EmailVO(email);
            Phone = new PhoneVO(phone);

            Validate();
        }

        private void Validate()
        {
            // Adiciona as notificações dos VOs na entidade
            AddNotifications(Name.Notifications);
            AddNotifications(Document.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Phone.Notifications);
        }

        public void ValidateIf(bool condition, string propertyName, string errorMessage)
        {
            if (condition)
                AddNotification(propertyName, errorMessage);
        }
    }
}
