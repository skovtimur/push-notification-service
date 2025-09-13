namespace PushNotificationService.Shared.Exceptions;

public class ErrorInfo
{
    public string ErrorMessage { get; private init; }
    public string? Source { get; private init; }
    public string? HelpLink { get; private init; }
    public string? StackTrace { get; private init; }
    public ErrorInfo? InnerException { get; private init; }


    public ErrorInfo(string exceptionMessage, Exception exception)
    {
        ErrorMessage = exception.Message;
        StackTrace = exception.StackTrace;
        Source = exception.Source;
        HelpLink = exception.HelpLink;
        ErrorMessage = exceptionMessage;
        InnerException = GetInnerError(exception);
    }

    public ErrorInfo(Exception exception)
    {
        ErrorMessage = exception.Message;
        StackTrace = exception.StackTrace;
        Source = exception.Source;
        HelpLink = exception.HelpLink;
        InnerException = GetInnerError(exception);
    }

    public ErrorInfo(string exceptionMessage)
    {
        ErrorMessage = exceptionMessage;
    }

    private static ErrorInfo? GetInnerError(Exception? exception)
    {
        var innerException = exception?.InnerException;

        return innerException == null
            ? null
            : new ErrorInfo(innerException);
    }
}