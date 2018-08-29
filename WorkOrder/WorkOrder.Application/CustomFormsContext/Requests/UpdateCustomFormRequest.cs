using System;

namespace WorkOrder.Application.CustomFormsContext.Requests
{
    public class UpdateCustomFormRequest : CreateCustomFormRequest
    {
        public Guid? Id { get; set; }

        public override bool IsValid()
        {
            return Id.HasValue;
        }
    }
}
