using System;
using Flunt.Validations;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class CustomFieldAnswer : TenantEntity
    {
        protected CustomFieldAnswer()
        {

        }
        public CustomFieldAnswer(Guid customFieldId, string answer, Guid customFormAnswerId, Guid? customFieldOptionId, Guid tenantId)
        {

            CustomFieldId = customFieldId;
            Answer = answer;
            CustomFieldOptionId = customFieldOptionId;
            CustomFormAnswerId = customFormAnswerId;
            SetTenantId(tenantId);

            if (customFieldOptionId != null)
            {
                if (string.IsNullOrEmpty(answer))
                    Answer = customFieldOptionId.Value.ToString();
            }

            new Contract()
                .Requires()
                .IsNullOrEmpty(Answer, "Answer", "A resposta deve ser informada");
        }

        public CustomFormAnswer CustomFormAnswer { get; private set; }
        public Guid CustomFormAnswerId { get; private set; }
        public CustomField CustomField { get; private set; }
        public Guid CustomFieldId { get; private set; }
        public string Answer { get; private set; }
        public Guid? CustomFieldOptionId { get; private set; }
        public CustomFieldOption CustomFieldOption { get; private set; }

    }
}
