using System;
using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.CustomFormsContext.Requests
{
    public class SendCustomFieldRequest : ICommand
    {
        public Guid CustomFieldId { get; set; }
        public string Answer { get; set; }
        public Guid? CustomFieldOptionId { get; set; }
        public bool IsValid()
        {
            return true;
        }
    }
}
