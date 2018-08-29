using System;
using System.Collections.Generic;
using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.CustomFormsContext.Requests
{
    public class SendCustomFormRequest : ICommand
    {
        public Guid CustomFormId { get; set; }

        public List<SendCustomFieldRequest> Fields { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
