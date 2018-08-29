using Flunt.Notifications;
using Flunt.Validations;

namespace WorkOrder.Domain.SharedContext.ValueObjects
{
    public class EmailVo : Notifiable, IValidatable
    {

        protected EmailVo()
        {

        }

        public EmailVo(string address)
        {
            Address = address;
            Validate();
        }

        public string Address { get; private set; }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public void Validate()
        {
            AddNotifications(new Contract().Requires()
                .IsNotNullOrEmpty(Address, "Email", "O endereço de E-mail é obrigatório"));
        }
    }
}
