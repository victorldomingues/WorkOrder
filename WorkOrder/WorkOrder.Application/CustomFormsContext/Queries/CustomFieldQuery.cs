using System;
using System.Collections.Generic;
using WorkOrder.Domain.CustomFormsContext.Enums;

namespace WorkOrder.Application.CustomFormsContext.Queries
{
    public class CustomFieldQuery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public EFieldType Type { get; set; }
        public Guid? SelectedOptionId { get; set; }
        public string Answer { get; set; }
        public IEnumerable<CustomFieldOptionQuery> Options { get; set; }
    }
}
