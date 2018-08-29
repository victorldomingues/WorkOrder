using System;
using Flunt.Validations;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class CustomFieldOption : TenantEntity
    {

        protected CustomFieldOption()
        {

        }

        public CustomFieldOption(string name, Guid customFieldId, Guid tenantId)
        {
            Name = name;
            CustomFieldId = customFieldId;
            SetTenantId(tenantId);
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "O Nome do componente não foi informado.")
                .HasMaxLen(Name, 255, "Name", "O Nome deve ter no máximo 255 caracteres"))
                ;
        }

        public string Name { get; private set; }

        public CustomField CustomField { get; private set; }

        public Guid CustomFieldId { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }


    }
}
