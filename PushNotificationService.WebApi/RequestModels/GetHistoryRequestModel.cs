using System.ComponentModel.DataAnnotations;
using PushNotificationService.Application.Features.Notifications.GetHistory;

namespace PushNotificationService.WebApi.RequestModels;

public class GetHistoryRequestModel
{
    public DateTime? FromUtc { get; set; }
    public DateTime? ToUtc { get; set; }

    [Range(1, GetHistoryQueryValidator.LimitMaximumLength)]
    public int? Limit { get; set; }
}