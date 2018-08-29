using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.SharedContext.ValueObjects;

namespace WorkOrder.Infra.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder.ToTable("Users");
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);
            builder
                .Property(x => x.Id)
                .HasMaxLength(36);

            builder.OwnsOne(s => s.Name, cb =>
            {
                cb.Property(c => c.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(255)
                    .IsRequired();

                cb.Property(c => c.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(255)
                    .IsRequired();
            });



            builder.OwnsOne(s => s.Email, cb =>
            {
                cb
                    .Property(x => x.Address)
                    .HasColumnName("Email")
                    .HasMaxLength(255)
                    .IsRequired(true);
                cb
                    .HasIndex(x => x.Address)
                    .IsUnique();
            });

            builder.OwnsOne(s => s.Phone, cb =>
            {
                cb
                    .Property(x => x.Number)
                    .HasColumnName("Phone")
                    .IsRequired(false);

            });

            builder.OwnsOne(s => s.Password, cb =>
            {
                cb
                    .Property(x => x.Password)
                    .HasColumnName("Password")
                    .HasMaxLength(30)
                    .IsRequired(true);

            });

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
