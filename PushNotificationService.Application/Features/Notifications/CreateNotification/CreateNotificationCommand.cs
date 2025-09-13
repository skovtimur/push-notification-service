using MediatR;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Application.Features.Notifications.CreateNotification;

public class CreateNotificationCommand : IRequest<Guid>
{
    public required string Username { get; init; }
    public required string Title { get; init; }
    public string? Text { get; init; }
    public UserEntity User { get; init; }

    public string UpperUsername => Username.ToUpper();

    public static CreateNotificationCommand Create(string username, UserEntity user, string title, string? text = null)
    {
        var command = new CreateNotificationCommand { Username = username, User = user, Title = title, Text = text };
        return CreateNotificationCommandValidator.ThrowIfInvalid(command);
    }
}