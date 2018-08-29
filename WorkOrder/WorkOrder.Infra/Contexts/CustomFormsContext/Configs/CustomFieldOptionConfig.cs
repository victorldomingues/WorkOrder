using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Configs
{
    class CustomFieldOptionConfig : IEntityTypeConfiguration<CustomFieldOption>
    {
        public void Configure(EntityTypeBuilder<CustomFieldOption> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("CustomFieldOptions", "dbo");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
