using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.SharedContext.Entities;

namespace WorkOrder.Infra.Configs
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("Address", "dbo");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Id).HasMaxLength(36);
        }
    }
}
