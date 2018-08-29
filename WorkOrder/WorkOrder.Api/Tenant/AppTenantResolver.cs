using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WorkOrder.Infra.Contexts;
using WorkOrder.Shared.Entities;
using SaasKit.Multitenancy;

namespace WorkOrder.Api.Tenant
{
    public class AppTenantResolver : MemoryCacheTenantResolver<AppTenant>
    {
        private readonly ApplicationContext _context;

        public AppTenantResolver(IMemoryCache cache, ILoggerFactory loggerFactory, ApplicationContext context)
            : base(cache, loggerFactory)
        {
            _context = context;
        }

        protected override Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<AppTenant> tenantContext = null;
            var hostName = context.Request.Host.Value.ToLower();

#if DEBUG
            hostName = hostName.Split(":").Last();
#endif

            var tenant = _context.AppTenants.FirstOrDefault(
                t => t.Hostname.Equals(hostName));

            tenantContext = tenant != null ? new TenantContext<AppTenant>(tenant) : new TenantContext<AppTenant>(new AppTenant("dbo", "dbo", "default", null));

            _context.Dispose();

            return Task.FromResult(tenantContext);
        }

        protected override string GetContextIdentifier(HttpContext context)
        {
            return context.Request.Host.Value.ToLower();
        }

        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<AppTenant> context)
        {
            return new[] { context.Tenant.Hostname };
        }
    }
}
