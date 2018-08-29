using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.AccountContext.Repositories;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.AccountContext.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IDbConnection _connection;

        private readonly SaasContext _context;

        public TenantRepository(SaasContext context)
        {
            _context = context;
            _connection = _context.Database.GetDbConnection();
        }

        public void Save(AppTenant entity)
        {
            _context.AppTenants.Add(entity);
            //CreateTenantSchema(entity.Hostname);
        }

        public AppTenant Get(string hostname)
        {
            return _context.AppTenants.FirstOrDefault(x => x.Hostname.Equals(hostname));
        }

    }
}
