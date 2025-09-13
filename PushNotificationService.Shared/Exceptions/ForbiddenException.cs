namespace PushNotificationService.Shared.Exceptions;

public class ForbiddenException(string message) : Exception(message), IExceptionToResultMapper
{
    public ErrorResultDto ToResultDto(int statusCode)
    {
        return ErrorResultDto.Fail(new ErrorInfo($"Access denied: {Message}", this),
            statusCode);
    }
}