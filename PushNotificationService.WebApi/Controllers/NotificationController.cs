using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Application.Features.Notifications.CreateNotification;
using PushNotificationService.Application.Features.Notifications.GetHistory;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Shared.Exceptions;
using PushNotificationService.WebApi.Filters;
using PushNotificationService.WebApi.RequestModels;

namespace PushNotificationService.WebApi.Controllers;

[ApiController]
[Route("/api/notifications/")]
public class NotificationController(IMediator mediator, IUserRepository userRepository) : ControllerBase
{
    [HttpGet("history/{username}"), ValidationFilter]
    public async Task<IActionResult> GetHistory(
        [Required,
         StringLength(CreateUserCommandValidator.UsernameMaximumLength,
             MinimumLength = CreateUserCommandValidator.UsernameMinimumLength)]
        string username,
        [Required, FromQuery] GetHistoryRequestModel request)
    {
        var isExisting = await userRepository.Exists(username);

        if (isExisting == false)
            return NotFound(ErrorResultDto.Fail($"User doesn't exist with the username: {username}", 404));

        var history = await mediator.Send(new GetHistoryQuery
        {
            Username = username,
            Limit = request.Limit,
            FromUtc = request.FromUtc,
            ToUtc = request.ToUtc
        });

        return Ok(history);
    }

    [HttpPost("send"), ValidationFilter]
    public async Task<IActionResult> Send([FromBody] SendNotificationRequestModel request)
    {
        var user = await userRepository.GetByUsername(request.Username);

        if (user == null)
            return NotFound(ErrorResultDto.Fail($"User doesn't exist with the username: {request.Username}", 404));

        var command = CreateNotificationCommand.Create(username: request.Username, user: user,
            title: request.Title, text: request.Text);

        var id = await mediator.Send(command);
        return Ok(id);
    }
}