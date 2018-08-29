using AutoMapper;
using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Domain.SharedContext.Enums;
using WorkOrder.Shared.ValidationContracts;

namespace WorkOrder.Domain.SharedContext.ValueObjects
{
    public class PhoneVo : Notifiable, IValidatable
    {
        public PhoneVo(string number)
        {
            Type = EContactType.Default;
            Number = number;
            Validate();
        }

        public PhoneVo(string number, EContactType type) : this(number)
        {
            Type = type;
        }

        protected PhoneVo()
        {

        }

        public string Number { get; private set; }
        public EContactType Type { get; private set; }

        public void Validate()
        {
            AddNotifications(new Contract().Requires().Join(ValidationContract.Phone(Number)));
        }

    }
}
