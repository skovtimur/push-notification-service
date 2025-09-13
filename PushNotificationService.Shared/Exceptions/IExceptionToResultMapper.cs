namespace PushNotificationService.Shared.Exceptions;

public interface IExceptionToResultMapper
{
    public ErrorResultDto ToResultDto(int statusCode);
}