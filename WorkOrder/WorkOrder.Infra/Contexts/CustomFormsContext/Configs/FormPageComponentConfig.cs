using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Configs
{
    public class FormPageComponentConfig : IEntityTypeConfiguration<FormPageComponent>
    {
        public void Configure(EntityTypeBuilder<FormPageComponent> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("FormPageComponents", "dbo");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
