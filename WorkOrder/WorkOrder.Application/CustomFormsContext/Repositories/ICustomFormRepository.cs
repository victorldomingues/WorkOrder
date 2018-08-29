using System;
using System.Collections.Generic;
using WorkOrder.Application.CustomFormsContext.Queries;
using WorkOrder.Application.SharedContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Application.CustomFormsContext.Repositories
{
    public interface ICustomFormRepository : IRepository<CustomForm>
    {

        IEnumerable<CustomFormQuery> Query();

        CustomFormQuery Query(Guid id);

    }
}
