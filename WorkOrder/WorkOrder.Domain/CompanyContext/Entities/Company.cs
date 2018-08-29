using System;
using AutoMapper;
using Flunt.Validations;
using WorkOrder.Domain.SharedContext.Entities;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Shared.Entities;
using WorkOrder.Shared.ValidationContracts;

namespace WorkOrder.Domain.CompanyContext.Entities
{
    public class Company : TenantEntity, IValidatable
    {

        protected Company()
        {

        }
        public Company(Guid tenantId, string name, DocumentVo document, PhoneVo phone, EmailVo emailVo, Address address, Guid addressId, string site)
        {

            SetTenantId(tenantId);

            Name = name;
            Document = document;
            Phone = phone;
            Email = emailVo;
            Address = address;
            AddressId = addressId;
            Site = site;

        }



        public string Name { get; private set; }
        public DocumentVo Document { get; private set; }
        public PhoneVo Phone { get; private set; }
        public EmailVo Email { get; private set; }
        public Address Address { get; private set; }
        public Guid AddressId { get; private set; }
        public string Site { get; private set; }


        public void Validate()
        {
            AddNotifications(
                ValidationContract.Name(Name)
                .Join(ValidationContract.Site(Site))
                );
            AddNotifications(Document.Notifications);
            AddNotifications(Phone.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Document.Notifications);
        }
    }
}
