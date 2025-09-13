using FluentValidation;

namespace PushNotificationService.Shared.Domain.ValueObjects;

public class DeviceTokenValidator : AbstractValidator<DeviceTokenValueObject>
{
    public DeviceTokenValidator()
    {
        RuleFor(x => x.DeviceToken).NotEmpty().NotNull()
            .MinimumLength(DeviceTokenMinimumLength)
            .MaximumLength(DeviceTokenMaximumLength);
    }

    public const int DeviceTokenMaximumLength = 100000;
    public const int DeviceTokenMinimumLength = 3;

    public static DeviceTokenValueObject ThrowIfInvalid(DeviceTokenValueObject token)
    {
        var result = Validator.Validate(token);

        if (result.IsValid)
            return token;

        throw new ValidationException(result.Errors);
    }

    private static readonly DeviceTokenValidator Validator = new();
}