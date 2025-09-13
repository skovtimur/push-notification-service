using PushNotificationService.Shared.Domain.ValueObjects;

namespace PushNotificationService.Shared.Domain.Entities;

public class UserEntity : BaseEntity
{
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
    public required DeviceTokenValueObject DeviceToken { get; init; }

    public string UpperUsername { get; init; }
    public List<NotificationEntity> Notifications { get; init; } = [];
}