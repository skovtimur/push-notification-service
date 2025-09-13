using FluentValidation;
using PushNotificationService.Application.Features.Users.CreateUser;

namespace PushNotificationService.Application.Features.Notifications.GetHistory;

public class GetHistoryQueryValidator : AbstractValidator<GetHistoryQuery>
{
    public GetHistoryQueryValidator()
    {
        RuleFor(x => x.Limit).Must(x => x == null
                                        || (x >= 1 && x < LimitMaximumLength));

        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Username).NotEmpty()
            .MinimumLength(CreateUserCommandValidator.UsernameMinimumLength)
            .MaximumLength(CreateUserCommandValidator.UsernameMaximumLength);
    }

    public const int LimitMaximumLength = int.MaxValue;

    public static GetHistoryQuery ThrowIfInvalid(GetHistoryQuery query)
    {
        var result = Validator.Validate(query);

        if (result.IsValid)
            return query;

        throw new ValidationException(result.Errors);
    }

    private static readonly GetHistoryQueryValidator Validator = new();
}