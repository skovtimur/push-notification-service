namespace PushNotificationService.Shared.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? RemovedAt { get; private set; }
    public bool IsDeleted { get; private set; }


    public void RemoveEntity()
    {
        RemovedAt = DateTime.UtcNow;
        IsDeleted = true;
    }

    protected void UpdateEntity()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}