using FluentValidation;
using PushNotificationService.Shared.Domain.ValueObjects;

namespace PushNotificationService.Application.Features.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Username).NotEmpty().NotNull()
            .MaximumLength(UsernameMaximumLength).MinimumLength(UsernameMinimumLength);
        RuleFor(u => u.PasswordHash).NotEmpty().NotNull();

        RuleFor(u => u.DeviceToken).SetValidator(new DeviceTokenValidator());
    }

    public const int UsernameMaximumLength = 24;
    public const int UsernameMinimumLength = 3;

    public static CreateUserCommand ThrowIfInvalid(CreateUserCommand command)
    {
        var result = Validator.Validate(command);

        if (result.IsValid)
            return command;

        throw new ValidationException(result.Errors);
    }

    private static readonly CreateUserCommandValidator Validator = new();
}