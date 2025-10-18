using SistemaImobiliario.Domain.Notifications;

namespace SistemaImobiliario.Domain.ValueObjects
{
    public class NameVO : Notifiable
    {
        public string Name { get; }

        public NameVO(string name)
        {
            Name = name;
            Validate();
        }

        private void Validate()
        {
            ClearNotifications();

            if (!ValidationExtensions.ValidateRequired(Name,nameof(Name),"Emaill é obrigatório.",
                n => AddNotification(n.Property, n.Message)))
                return;

            // Adicione mais regras de Nome aqui...
        }

        public override string ToString() => Name;
    }
}
