using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Configs
{
    class PageComponentConfig : IEntityTypeConfiguration<PageComponent>
    {
        public void Configure(EntityTypeBuilder<PageComponent> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("PageComponents", "dbo");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder
                .Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired(true);
        }
    }
}
