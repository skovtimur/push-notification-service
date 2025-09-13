using MediatR;
using PushNotificationService.Shared.Domain.ValueObjects;

namespace PushNotificationService.Application.Features.Users.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
    public required DeviceTokenValueObject DeviceToken { get; init; }
    public string UpperUsername => Username.ToUpper();

    public static CreateUserCommand Create(string username, string passwordHash, DeviceTokenValueObject deviceToken)
    {
        var command = new CreateUserCommand
            { Username = username, PasswordHash = passwordHash, DeviceToken = deviceToken };

        return CreateUserCommandValidator.ThrowIfInvalid(command);
    }
}