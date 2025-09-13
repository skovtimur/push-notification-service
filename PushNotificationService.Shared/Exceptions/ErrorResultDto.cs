namespace PushNotificationService.Shared.Exceptions;

public readonly struct ErrorResultDto()
{
    public ErrorInfo? Error { get; private init; }
    public DateTime CreatedAtUtc { get; private init; } = DateTime.UtcNow;
    public int? StatusCode { get; private init; }

    public static ErrorResultDto Fail(ErrorInfo error, int? statusCode = null)
    {
        return new ErrorResultDto
        {
            Error = error,
            CreatedAtUtc = DateTime.UtcNow,
            StatusCode = statusCode
        };
    }

    public static ErrorResultDto Fail(string errorMessage, int? statusCode = null)
    {
        return new ErrorResultDto
        {
            Error = new ErrorInfo(errorMessage),
            CreatedAtUtc = DateTime.UtcNow,
            StatusCode = statusCode
        };
    }
}