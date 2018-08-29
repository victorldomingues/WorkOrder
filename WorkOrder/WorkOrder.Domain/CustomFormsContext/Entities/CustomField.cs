using System;
using System.Collections.Generic;
using Flunt.Validations;
using WorkOrder.Domain.CustomFormsContext.Enums;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class CustomField : TenantEntity
    {

        protected CustomField()
        {

        }

        public CustomField(string name, string description, EFieldType type, Guid customFormId, bool mandatory, Guid tenantId)
        {
            Name = name;
            Description = description;
            Type = type;
            CustomFormId = customFormId;
            SetTenantId(tenantId);
            Mandatory = mandatory;
            new Contract()
               .Requires()
               .IsNullOrEmpty(Name, "Name", "O Nome do componente não foi informado.")
               .HasMaxLen(Name, 255, "Name", "O Nome deve ter no máximo 255 caracteres")
               .IsNull(Type, "Type", "O tipo do campo deve ser informado.")
               ;
            Options = new List<CustomFieldOption>();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public EFieldType Type { get; private set; }
        public ICollection<CustomFieldOption> Options { get; }
        public bool Mandatory { get; private set; }

        public CustomForm CustomForm { get; private set; }
        public Guid CustomFormId { get; private set; }

        public void AddOption(CustomFieldOption option)
        {
            Options.Add(option);
        }

        public void SetType(EFieldType type)
        {
            Type = type;
        }

        public void SetName(string name)
        {

            if (string.IsNullOrEmpty(name))
                AddNotification("Name", "O Nome do componente não foi informado.");

            Name = name;

        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public static bool HasOptions(EFieldType type)
        {
            var verify = false;

            switch (type)
            {
                case EFieldType.ComboBox:
                    verify = true;
                    break;
                case EFieldType.Radio:
                    verify = true;
                    break;
                default:
                    verify = false;
                    break;
            }
            return verify;
        }

        public bool HasOptions()
        {
            return HasOptions(Type);
        }
    }
}
