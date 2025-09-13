using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.Property(x => x.Username).IsRequired().HasColumnName("username")
            .HasMaxLength(CreateUserCommandValidator.UsernameMaximumLength);
        builder.Property(x => x.UpperUsername).IsRequired().HasColumnName("upper_username")
            .HasMaxLength(CreateUserCommandValidator.UsernameMaximumLength);
        builder.HasIndex(x => x.UpperUsername).HasDatabaseName("ix_user_upper_username");

        builder.Property(x => x.PasswordHash).IsRequired().HasColumnName("password_hash");
        builder.OwnsOne(x => x.DeviceToken,
            deviceTokenBuilder =>
            {
                deviceTokenBuilder.Property(x => x.DeviceToken)
                    .IsRequired().HasColumnName("device_token");
            });

        builder.HasMany(x => x.Notifications).WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}