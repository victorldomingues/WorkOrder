using System.Collections.ObjectModel;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Shared.Tenant
{
    public class MultitenancyOptions
    {
        public Collection<AppTenant> Tenants { get; set; }
    }
}
