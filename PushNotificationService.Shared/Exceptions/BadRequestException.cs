namespace PushNotificationService.Shared.Exceptions;

public class BadRequestException(string text) : Exception(text), IExceptionToResultMapper
{
    public ErrorResultDto ToResultDto(int statusCode)
    {
        return ErrorResultDto.Fail(new ErrorInfo(Message, this), statusCode);
    }
}