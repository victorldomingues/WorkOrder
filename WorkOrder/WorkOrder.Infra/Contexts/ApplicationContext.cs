using Microsoft.EntityFrameworkCore;
using WorkOrder.Infra.Configs;
using WorkOrder.Shared;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<AppTenant> AppTenants { get; set; }

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new AppTenantConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }

        #endregion
    }
}
