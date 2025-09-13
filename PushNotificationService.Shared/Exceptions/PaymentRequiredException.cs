namespace PushNotificationService.Shared.Exceptions;

public class PaymentRequiredException(string message) : Exception(message), IExceptionToResultMapper
{
    public ErrorResultDto ToResultDto(int statusCode)
    {
        return ErrorResultDto.Fail(new ErrorInfo($"Payment Required: {Message}", this), statusCode);
    }
}