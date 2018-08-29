using System;
using System.Collections;
using System.Collections.Generic;
using Flunt.Validations;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class CustomFormAnswer : TenantEntity
    {
        protected CustomFormAnswer()
        {
            
        }
        public CustomFormAnswer(Guid customFormId, Guid tenantId, Guid? userId = null)
        {
            UserId = userId;
            CustomFormId = customFormId;
            SetTenantId(tenantId);
            CustomFieldAnswers =  new List<CustomFieldAnswer>();

            new Contract()
                .Requires()
                .IsNull(CustomFormId, "CustomFormId", "O Id do formulário deve ser informado");
        }
        public CustomForm CustomForm { get; private set; }
        public Guid CustomFormId { get; private set; }
        public User User { get; private set; }
        public Guid? UserId { get; private set; }

        public ICollection<CustomFieldAnswer> CustomFieldAnswers { get; }

        public void AddField(CustomFieldAnswer customFieldAnswer)
        {
            CustomFieldAnswers.Add(customFieldAnswer);
        }
    }
}
