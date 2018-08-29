using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class FormPageComponentRepository : Repository , IFormPageComponentRepository
    {
        public FormPageComponentRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }

        public void Save(FormPageComponent entity)
        {
            Context.FormPageComponents.Add(entity);
        }

        public void Update(FormPageComponent entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
