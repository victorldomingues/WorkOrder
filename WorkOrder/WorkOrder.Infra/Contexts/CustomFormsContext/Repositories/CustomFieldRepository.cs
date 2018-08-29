using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class CustomFieldRepository : Repository, ICustomFieldRepository
    {
        public CustomFieldRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }

        public void Save(CustomField entity)
        {
            Context.CustomFields.Add(entity);
        }

        public void Update(CustomField entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public CustomField Get(Guid customFieldId)
        {
            return Context.CustomFields.FirstOrDefault(x => x.Id == customFieldId && x.TenantId == Tenant.Id);
        }
    }
}
