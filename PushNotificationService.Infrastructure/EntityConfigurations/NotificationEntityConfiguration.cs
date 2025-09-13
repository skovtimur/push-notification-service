using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PushNotificationService.Application.Features.Notifications.CreateNotification;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Infrastructure.EntityConfigurations;

public class NotificationEntityConfiguration : IEntityTypeConfiguration<NotificationEntity>
{
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.ToTable("notifications");
        builder.Property(x => x.Text).IsRequired().HasColumnName("text")
            .HasMaxLength(CreateNotificationCommandValidator.TextMaximumLength);
        builder.Property(x => x.Title).HasColumnName("title")
            .HasMaxLength(CreateNotificationCommandValidator.TitleMaximumLength);

        builder.Property(x => x.Username).IsRequired().HasColumnName("username")
            .HasMaxLength(CreateUserCommandValidator.UsernameMaximumLength);
        builder.Property(x => x.UpperUsername).IsRequired().HasColumnName("upper_username")
            .HasMaxLength(CreateUserCommandValidator.UsernameMaximumLength);
        builder.HasIndex(x => x.UpperUsername).HasDatabaseName("ix_notification_upper_username");

        builder.Property(x => x.UserId).IsRequired().HasColumnName("userId");
    }
}