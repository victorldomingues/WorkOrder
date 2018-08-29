using System;
using Flunt.Validations;
using WorkOrder.Domain.SharedContext.Entities;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.WorkOrderContext.Entities
{
    public class Customer : TenantEntity, IValidatable
    {


        protected Customer()
        {

        }


        public Customer(Guid tenantId, int code, NameVo name, PhoneVo phone, PhoneVo phone2, EmailVo email, DocumentVo document, Guid addressId, string ie)
        {
            SetTenantId(tenantId);
            Code = code;
            Name = name;
            Phone = phone;
            Phone2 = phone2;
            Email = email;
            Document = document;
            AddressId = addressId;
            Ie = ie;
        }


        public int Code { get; private set; }
        public NameVo Name { get; private set; }
        public PhoneVo Phone { get; private set; }
        public PhoneVo Phone2 { get; private set; }
        public EmailVo Email { get; private set; }
        public DocumentVo Document { get; private set; }
        public Address Address { get; set; }
        public Guid AddressId { get; private set; }
        public string Ie { get; private set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(Code, 0, "Code", "Código não pode ser igual a 0"))
                ;
            AddNotifications(Name.Notifications);
            AddNotifications(Phone.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Document.Notifications);
            //AddNotifications(Address.Notifications);
        }
    }
}
