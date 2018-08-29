using System;
using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.CustomFormsContext.Requests
{
    public class CreateCustomFieldOptionRequest : ICommand
    {

        public string Name { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
