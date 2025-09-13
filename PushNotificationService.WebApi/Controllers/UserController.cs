using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Application.Abstraction.ServiceInterfaces;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Shared.Domain.Dtos;
using PushNotificationService.Shared.Domain.ValueObjects;
using PushNotificationService.Shared.Exceptions;
using PushNotificationService.WebApi.Filters;
using PushNotificationService.WebApi.RequestModels;

namespace PushNotificationService.WebApi.Controllers;

[ApiController]
[Route("/api/users/")]
public class UserController(IMediator mediator, IHash hash, IUserRepository userRepository, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{username}"), ValidationFilter]
    public async Task<IActionResult> GetUser(
        [Required,
         StringLength(CreateUserCommandValidator.UsernameMaximumLength,
             MinimumLength = CreateUserCommandValidator.UsernameMinimumLength)]
        string username)
    {
        var user = await userRepository.GetByUsername(username);

        if (user == null)
            return NotFound(ErrorResultDto.Fail($"User doesn't exist with the username: {username}", 404));

        var mappedUser = mapper.Map<UserDtoToView>(user);
        return Ok(mappedUser);
    }

    [HttpPost("register"), ValidationFilter]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestModel request)
    {
        var deviceToken = DeviceTokenValueObject.Create(request.DeviceToken);

        var passwordHash = hash.CreateHash(request.Password);
        var command = CreateUserCommand.Create(request.Username, passwordHash, deviceToken);

        var id = await mediator.Send(command);
        return Ok(id);
    }
}