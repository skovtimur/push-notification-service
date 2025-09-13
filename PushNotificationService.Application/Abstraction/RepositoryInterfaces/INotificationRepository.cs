using PushNotificationService.Shared.Domain.Dtos;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Application.Abstraction.RepositoryInterfaces;

public interface INotificationRepository
{
    public Task CreateNotification(NotificationEntity notification);

    public Task<List<NotificationDtoToView>> GetHistoryByUsername(string username, DateTime? startDate = null,
        DateTime? endDate = null,
        int? limit = null);
}