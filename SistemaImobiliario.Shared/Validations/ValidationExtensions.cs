using SistemaImobiliario.Shared.Notifications;

public static class ValidationExtensions
{
    public static bool ValidateRequired(string value, string propertyName, string message,
        Action<Notification> addNotification)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            addNotification(new Notification(propertyName, message));
            return false;
        }
        return true;
    }

    public static bool ValidateRegex(string value, string pattern, string propertyName, string message, Action<Notification> addNotification)
    {
        var regex = new System.Text.RegularExpressions.Regex(pattern);
        if (!regex.IsMatch(value))
        {
            addNotification(new Notification(propertyName, message));
            return false;
        }
        return true;
    }
}
