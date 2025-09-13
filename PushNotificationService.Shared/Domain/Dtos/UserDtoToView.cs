namespace PushNotificationService.Shared.Domain.Dtos;

public class UserDtoToView
{
    public Guid Id { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime? RemovedAt { get; init; }
    public bool IsDeleted { get; init; }

    public string Username { get; init; }
    public string UpperUsername { get; init; }
    public string DeviceToken { get; init; }
}