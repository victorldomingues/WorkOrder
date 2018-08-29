using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class CustomFormAnswerRepository : Repository, ICustomFormAnswerRepository
    {
        public CustomFormAnswerRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }

        public void Save(CustomFormAnswer entity)
        {
            Context.CustomFormAnswers.Add(entity);
        }

        public void Update(CustomFormAnswer entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
