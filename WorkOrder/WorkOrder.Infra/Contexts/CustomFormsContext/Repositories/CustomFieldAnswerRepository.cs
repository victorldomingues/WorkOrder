using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class CustomFieldAnswerRepository : Repository, ICustomFieldAnswerRepository
    {
        public CustomFieldAnswerRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }

        public void Save(CustomFieldAnswer entity)
        {
            Context.CustomFieldAnswers.Add(entity);
        }

        public void Update(CustomFieldAnswer entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
