using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Domain.SharedContext.Enums;

namespace WorkOrder.Domain.SharedContext.ValueObjects
{
    public class DocumentVo : Notifiable, IValidatable
    {

        protected DocumentVo()
        {

        }

        public DocumentVo(string number, EDocumentType type)
        {
            Number = number;
            Type = type;
            Validate();
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        public void SetDocument(string number, EDocumentType type)
        {
            Number = number;
            Type = type;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Number, "Number", "O Número do documento é obrigatório"));
        }

    }
}
