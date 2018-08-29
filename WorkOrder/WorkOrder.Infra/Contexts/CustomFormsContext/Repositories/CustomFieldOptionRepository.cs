using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class CustomFieldOptionRepository : Repository, ICustomFieldOptionRepository
    {
        public CustomFieldOptionRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }
        public void Save(CustomFieldOption entity)
        {
            Context.CustomFieldOptions.Add(entity);
        }

        public void Update(CustomFieldOption entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
