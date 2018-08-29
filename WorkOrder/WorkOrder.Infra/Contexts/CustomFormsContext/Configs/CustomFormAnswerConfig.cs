using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.CustomFormsContext.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Configs
{
    public class CustomFormAnswerConfig : IEntityTypeConfiguration<CustomFormAnswer>
    {
        public void Configure(EntityTypeBuilder<CustomFormAnswer> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("CustomFormAnswers", "dbo");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CustomForm).WithMany().HasForeignKey(x => x.CustomFormId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
