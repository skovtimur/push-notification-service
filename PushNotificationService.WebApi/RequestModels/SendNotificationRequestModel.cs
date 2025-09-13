using System.ComponentModel.DataAnnotations;
using PushNotificationService.Application.Features.Notifications.CreateNotification;
using PushNotificationService.Application.Features.Users.CreateUser;

namespace PushNotificationService.WebApi.RequestModels;

public class SendNotificationRequestModel
{
    [Required,
     StringLength(maximumLength: CreateUserCommandValidator.UsernameMaximumLength,
         MinimumLength = CreateUserCommandValidator.UsernameMinimumLength)]
    public required string Username { get; set; }

    [Required,
     StringLength(maximumLength: CreateNotificationCommandValidator.TitleMaximumLength,
         MinimumLength = CreateNotificationCommandValidator.TitleMinimumLength)]
    public required string Title { get; set; }

    [Required, StringLength(maximumLength: CreateNotificationCommandValidator.TextMaximumLength, MinimumLength = 0)]
    public string? Text { get; set; } = null;
}