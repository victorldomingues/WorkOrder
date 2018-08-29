using WorkOrder.Shared.Entities;

namespace WorkOrder.Application.AccountContext.Repositories
{
    public interface ITenantRepository
    {
        void Save(AppTenant entity);

        AppTenant Get(string hostname);
    }
}
