using MediatR;
using PushNotificationService.Shared.Domain.Dtos;

namespace PushNotificationService.Application.Features.Notifications.GetHistory;

public class GetHistoryQuery : IRequest<List<NotificationDtoToView>>
{
    public required string Username { get; init; }

    public DateTime? FromUtc { get; init; }
    public DateTime? ToUtc { get; init; }

    public int? Limit { get; init; }
}