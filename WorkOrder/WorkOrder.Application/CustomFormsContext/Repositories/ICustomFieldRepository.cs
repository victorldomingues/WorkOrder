using System;
using WorkOrder.Application.SharedContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Application.CustomFormsContext.Repositories
{
    public interface ICustomFieldRepository  : IRepository<CustomField>
    {
        CustomField Get(Guid customFieldId);
    }
}
