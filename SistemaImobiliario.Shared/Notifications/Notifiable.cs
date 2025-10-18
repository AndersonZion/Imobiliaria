

namespace SistemaImobiliario.Shared.Notifications
{
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications = [];

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        protected void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }


        protected void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public bool IsValid => !_notifications.Any();

        protected void ClearNotifications() => _notifications.Clear();

    }

}
