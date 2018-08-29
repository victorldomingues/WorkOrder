using System.Collections.Generic;
using WorkOrder.Domain.CustomFormsContext.Enums;
using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.CustomFormsContext.Requests
{
    public class CreateCustomFieldRequest : ICommand
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public EFieldType Type { get; set; }
        public bool Mandatory { get; set; }
        public List<CreateCustomFieldOptionRequest> Options { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return false;
            return true;
        }
    }
}
