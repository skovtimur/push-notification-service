namespace PushNotificationService.Shared.Domain.Entities;

public class NotificationEntity : BaseEntity
{
    public required string Title { get; init; }
    public required string Text { get; init; }
    public required string Username { get; init; }
    public string UpperUsername { get; init; }

    public Guid UserId { get; init; }
    public UserEntity User { get; init; }
}