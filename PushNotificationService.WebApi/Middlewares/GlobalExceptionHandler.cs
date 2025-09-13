using System.Net;
using PushNotificationService.Shared.Exceptions;

namespace PushNotificationService.WebApi.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogTrace("GlobalExceptionHandler Invoked");
        const int badRequestCode = (int)HttpStatusCode.BadRequest;

        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException ex)
        {
            await HandleExceptionAsync(ex.Message, context, badRequestCode, ex.ToResultDto(badRequestCode));
        }
        catch (FluentValidation.ValidationException ex)
        {
            await HandleExceptionAsync(ex.Message, context, badRequestCode,
                ErrorResultDto.Fail(new ErrorInfo(ex), badRequestCode));
        }
        catch (PaymentRequiredException ex)
        {
            const int paymentRequiredCode = (int)HttpStatusCode.PaymentRequired;
            await HandleExceptionAsync(ex.Message, context, paymentRequiredCode,
                ex.ToResultDto(paymentRequiredCode));
        }
        catch (NotFoundException ex)
        {
            const int notFoundCode = (int)HttpStatusCode.Forbidden;
            await HandleExceptionAsync(ex.Message, context, notFoundCode, ex.ToResultDto(notFoundCode));
        }
        catch (ForbiddenException ex)
        {
            const int forbiddenCode = (int)HttpStatusCode.Forbidden;
            await HandleExceptionAsync(ex.Message, context, forbiddenCode, ex.ToResultDto(forbiddenCode));
        }
    }

    private async Task HandleExceptionAsync(string exceptionMessage, HttpContext context, int statusCode,
        ErrorResultDto result)
    {
        logger.LogWarning(exceptionMessage);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(result);
    }
}