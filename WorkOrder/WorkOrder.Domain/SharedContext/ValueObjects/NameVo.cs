using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Shared.ValidationContracts;

namespace WorkOrder.Domain.SharedContext.ValueObjects
{
    public class NameVo : Notifiable, IValidatable
    {

        protected NameVo()
        {

        }

        public NameVo(string firstName, string lastName)
        {

            FirstName = firstName;
            LastName = lastName;
            Validate();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public void SetName(string firstName, string lastName)
        {

            FirstName = firstName;
            LastName = lastName;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .Join(ValidationContract.FirstNameLastName(FirstName, LastName)))
                ;
        }
    }
}
