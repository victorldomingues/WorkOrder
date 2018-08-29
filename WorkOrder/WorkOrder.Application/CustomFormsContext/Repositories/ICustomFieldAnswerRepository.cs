using WorkOrder.Application.SharedContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Application.CustomFormsContext.Repositories
{
    public interface ICustomFieldAnswerRepository  : IRepository<CustomFieldAnswer>
    {
    }
}
