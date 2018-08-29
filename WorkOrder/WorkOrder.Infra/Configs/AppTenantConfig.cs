using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Configs
{
    public class AppTenantConfig : IEntityTypeConfiguration<AppTenant>
    {
        public void Configure(EntityTypeBuilder<AppTenant> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder.ToTable("Tenants", "dbo");

            builder.Ignore(x => x.Notifications);

            builder.Ignore(x => x.Valid);

            builder.Ignore(x => x.Invalid);

            builder
                .Property(x => x.Id)
                .HasMaxLength(36);

            builder
                .Property(x => x.Hostname)
                .HasMaxLength(300)
                .IsRequired(true);

            builder
                .HasIndex(x => x.Hostname)
                .IsUnique();

            builder
                .Property(x => x.EntityStatus)
                .IsRequired(true);

            builder
                .Property(x => x.CreatedAt)
                .IsRequired(true);


            builder
                .Property(x => x.UpdatedAt)
                .IsRequired(false);


            builder
                .Property(x => x.DeletedAt)
                .IsRequired(false);
        }
    }
}
