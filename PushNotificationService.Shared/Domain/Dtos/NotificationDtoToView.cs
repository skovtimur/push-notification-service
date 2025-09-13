namespace PushNotificationService.Shared.Domain.Dtos;

public class NotificationDtoToView
{
    public Guid Id { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime? RemovedAt { get; init; }
    public bool IsDeleted { get; init; }


    public string Title { get; init; }
    public string Text { get; init; }
    public string Username { get; init; }
    public string UpperUsername { get; init; }
    public Guid UserId { get; set; }
}