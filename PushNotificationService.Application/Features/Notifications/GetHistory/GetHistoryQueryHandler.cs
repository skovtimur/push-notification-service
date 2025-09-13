using MediatR;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Shared.Domain.Dtos;

namespace PushNotificationService.Application.Features.Notifications.GetHistory;

public class GetHistoryQueryHandler(INotificationRepository notificationRepository)
    : IRequestHandler<GetHistoryQuery, List<NotificationDtoToView>>
{
    public async Task<List<NotificationDtoToView>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await notificationRepository.GetHistoryByUsername(
            username: request.Username,
            limit: request.Limit,
            startDate: request.FromUtc,
            endDate: request.ToUtc);

        return history;
    }
}