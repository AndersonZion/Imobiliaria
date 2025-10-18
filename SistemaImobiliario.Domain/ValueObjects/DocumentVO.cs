
using SistemaImobiliario.Domain.Notifications;

namespace SistemaImobiliario.Domain.ValueObjects
{
    public class DocumentVO : Notifiable
    {
        public DocumentVO(string document)
        {
            Document = document;
            Validate();
        }

        public string Document { get; }

        private void Validate()
        {
            ClearNotifications();

            if (!ValidationExtensions.ValidateRequired(
            Document,
            nameof(Document),
            "Documento é obrigatório.",
            n => AddNotification(n.Property, n.Message)))
                return;

            // if (string.IsNullOrWhiteSpace(Document))
            // {
            //     AddNotification(nameof(Document), "Document é obrigatório.");
            //     return; // Evita validar formato se está vazio
            // }

            if (!IsCpfValid(Document)) // 👈 AQUI É USADA
            {
                AddNotification(nameof(Document), "Documento inválido. CPF incorreto.");
            }
        }

        private bool IsCpfValid(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var temp = cpf[..9];
            var sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(temp[i].ToString()) * mult1[i];

            var remainder = sum % 11;
            var digit1 = remainder < 2 ? 0 : 11 - remainder;

            temp += digit1;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(temp[i].ToString()) * mult2[i];

            remainder = sum % 11;
            var digit2 = remainder < 2 ? 0 : 11 - remainder;

            return cpf.EndsWith($"{digit1}{digit2}");
        }

        public override string ToString() => Document;
    }
}