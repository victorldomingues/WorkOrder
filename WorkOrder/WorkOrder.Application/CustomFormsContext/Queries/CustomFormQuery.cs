using System;
using System.Collections.Generic;

namespace WorkOrder.Application.CustomFormsContext.Queries
{
    public class CustomFormQuery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CustomFieldQuery> Fields { get; set; }

    }
}
