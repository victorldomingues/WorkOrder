using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Application.SharedContext.Requests;
using WorkOrder.Domain.SharedContext.Enums;
using WorkOrder.Shared.ValidationContracts;

namespace WorkOrder.Application.WorkOrderContext.Requests
{
    public class CreateCustomerRequest : Notifiable, IValidatable
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Ie { get; set; }
        public string Document { get; set; }
        public EDocumentType DocumentType { get; set; }
        public CreateAddressRequest Address { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .Join(ValidationContract.FirstNameLastName(FirstName, LastName)
            .Join()
            ));
        }
    }
}
