using FluentValidation;

namespace PushNotificationService.Application.Features.Notifications.CreateNotification;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(TitleMaximumLength).MinimumLength(TitleMinimumLength).NotEmpty();
        RuleFor(x => x.Text).Must(x => x == null || x.Length <= TextMaximumLength);
        RuleFor(x => x.User).NotNull().NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
    }

    public const int TitleMaximumLength = 50;
    public const int TitleMinimumLength = 3;

    public const int TextMaximumLength = 5000;

    public static CreateNotificationCommand ThrowIfInvalid(CreateNotificationCommand command)
    {
        var result = Validator.Validate(command);

        if (result.IsValid)
            return command;

        throw new ValidationException(result.Errors);
    }

    private static readonly CreateNotificationCommandValidator Validator = new();
}