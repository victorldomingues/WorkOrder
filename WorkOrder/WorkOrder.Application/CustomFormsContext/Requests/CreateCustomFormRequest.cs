using System;
using System.Collections.Generic;
using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.CustomFormsContext.Requests
{
    public class CreateCustomFormRequest : ICommand
    {

        public string Name { get; set; }

        public List<CreateCustomFieldRequest> Fields { get; set; }

        public Guid? PageComponentId { get; set; }

        public virtual bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return false;
            return true;
        }
    }
}
