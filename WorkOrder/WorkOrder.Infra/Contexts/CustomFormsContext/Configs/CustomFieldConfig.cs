using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Configs
{
    public class CustomFieldConfig : IEntityTypeConfiguration<CustomField>
    {
        public void Configure(EntityTypeBuilder<CustomField> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("CustomFields", "dbo");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);
            builder
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired(true);

        }
    }
}
