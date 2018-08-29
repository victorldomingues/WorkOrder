using System;
using System.Collections.Generic;
using Flunt.Validations;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class CustomForm : TenantEntity
    {

        protected CustomForm()
        {

        }

        public CustomForm(string name, Guid tenantId)
        {

            Name = name;
            SetTenantId(tenantId);

            new Contract()
                .Requires()
                .IsNullOrEmpty(Name, "Name", "O Nome do formulário não foi informado.")
                .HasMaxLen(Name, 255, "Name", "O Nome deve ter no máximo 255 caracteres")
                ;

            Fields = new List<CustomField>();
        }

        public string Name { get; private set; }

        public ICollection<CustomField> Fields { get; }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                AddNotification("Name", "O Nome do formulário não foi informado.");

            Name = name;
        }

        public void AddField(CustomField field)
        {
            AddNotifications(field.Notifications);
            if (field.Valid)
                Fields.Add(field);
        }
    }
}
