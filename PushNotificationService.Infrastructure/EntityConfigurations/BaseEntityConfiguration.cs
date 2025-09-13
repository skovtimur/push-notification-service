using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushNotificationService.Shared;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Infrastructure.EntityConfigurations;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").IsRequired();

        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted")
            .IsRequired().HasDefaultValue(false);
        builder.Property(x => x.RemovedAt).HasColumnName("removed_at");

        builder.UseTpcMappingStrategy();
    }
}