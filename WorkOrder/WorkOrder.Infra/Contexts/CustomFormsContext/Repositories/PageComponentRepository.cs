using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class PageComponentRepository : Repository, IPageComponentRepository
    {
        public PageComponentRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }

        public void Save(PageComponent entity)
        {
            Context.PageComponents.Add(entity);
        }

        public void Update(PageComponent entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
