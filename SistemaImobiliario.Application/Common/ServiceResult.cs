using SistemaImobiliario.Domain.Notifications;


namespace SistemaImobiliario.Application.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IEnumerable<Notification>? Errors { get; private set; }

        public static ServiceResult<T> Ok(T data) => new ServiceResult<T> { Success = true, Data = data };
        public static ServiceResult<T> Fail(IEnumerable<Notification> errors) =>
            new ServiceResult<T> { Success = false, Errors = errors };

        public static ServiceResult<T> Fail(string property, string message) =>
            Fail(new[] { new Notification(property, message) });

        public static ServiceResult<T> Fail(params string[] messages) =>
            Fail(messages.Select(m => new Notification("General", m)));

    }
}
