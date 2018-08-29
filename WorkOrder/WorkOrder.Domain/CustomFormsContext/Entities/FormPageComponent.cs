using System;
using Flunt.Validations;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class FormPageComponent : TenantEntity
    {
        protected FormPageComponent()
        {

        }

        public FormPageComponent(Guid pageComponentId, Guid customFormId, Guid tenantId)
        {
            PageComponentId = pageComponentId;
            CustomFormId = customFormId;
            Order = 0;
            SetTenantId(tenantId);
            new Contract()
                .Requires()
                .IsNull(PageComponentId, "PageComponentId", "O Componente não foi informado.")
                .IsNull(CustomFormId, "CustomFormId", "O Formulário não foi informado.")
             ;
        }

        public FormPageComponent(Guid pageComponentId, Guid customFormId, int order, Guid tenantId) :
            this(pageComponentId,  customFormId, tenantId)
        {
            
            Order = order;
        }


        public PageComponent PageComponent { get; private set; }
        public Guid PageComponentId { get; private set; }
        public CustomForm CustomForm { get; private set; }
        public Guid CustomFormId { get; private set; }
        public int Order { get; private set; }

        public void SetOrder(int order)
        {
            Order = order;
        }

        public void SetComponent(Guid pageComponentId)
        {
            PageComponentId = pageComponentId;
        }

        public void SetCustomForm(Guid customFormId)
        {

            CustomFormId = customFormId;
        }
    }
}
