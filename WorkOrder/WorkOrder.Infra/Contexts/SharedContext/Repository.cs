using System.Data;
using Microsoft.EntityFrameworkCore;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.SharedContext
{
    public class Repository
    {
        public Repository(SaasContext context, AppTenant tenant = null)
        {
            Context = context;
            Connection = Context.Database.GetDbConnection();
            Tenant = tenant;
        }

        protected SaasContext Context { get; }
        protected IDbConnection Connection { get; }
        protected AppTenant Tenant { get; }
    }
}
