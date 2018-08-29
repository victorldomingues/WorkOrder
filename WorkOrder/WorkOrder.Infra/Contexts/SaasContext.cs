using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Domain.SharedContext.Entities;
using WorkOrder.Infra.Configs;
using WorkOrder.Infra.Contexts.CustomFormsContext.Configs;
using WorkOrder.Shared;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts
{
    public class SaasContext : DbContext
    {
        //public AppTenant Tenant { get; private set; }

        public SaasContext(DbContextOptions<SaasContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AppTenant> AppTenants { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<CustomFieldOption> CustomFieldOptions { get; set; }
        public DbSet<FormPageComponent> FormPageComponents { get; set; }
        public DbSet<PageComponent> PageComponents { get; set; }
        public DbSet<CustomFieldAnswer> CustomFieldAnswers { get; set; }
        public DbSet<CustomFormAnswer> CustomFormAnswers { get; set; }

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new AppTenantConfig());
            modelBuilder.ApplyConfiguration(new AddressConfig());
            modelBuilder.ApplyConfiguration(new CustomFormConfig());
            modelBuilder.ApplyConfiguration(new CustomFieldConfig());
            modelBuilder.ApplyConfiguration(new CustomFieldOptionConfig());
            modelBuilder.ApplyConfiguration(new PageComponentConfig());
            modelBuilder.ApplyConfiguration(new FormPageComponentConfig());
            modelBuilder.ApplyConfiguration(new CustomFormAnswerConfig());
            modelBuilder.ApplyConfiguration(new CustomFieldAnswerConfig());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(Settings.ConnectionString);
            base.OnConfiguring(optionsBuilder);


            //// this block forces map method invoke for each instance
            //var builder = new ModelBuilder(ConventionSet.CreateConventionSet(this));

            //OnModelCreating(builder);

            //optionsBuilder.UseModel(builder.Model);
        }

        #endregion
    }
}
