namespace PushNotificationService.Shared.Exceptions;

public class NotFoundException : Exception, IExceptionToResultMapper
{
    public NotFoundException(string str) : base(str)
    {
    }

    public ErrorResultDto ToResultDto(int statusCode)
    {
        return ErrorResultDto.Fail(new ErrorInfo(Message, this), statusCode);
    }
}