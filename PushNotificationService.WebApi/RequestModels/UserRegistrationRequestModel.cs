using System.ComponentModel.DataAnnotations;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Shared.Domain.ValueObjects;

namespace PushNotificationService.WebApi.RequestModels;

public class UserRegistrationRequestModel
{
    [Required,
     StringLength(maximumLength: CreateUserCommandValidator.UsernameMaximumLength,
         MinimumLength = CreateUserCommandValidator.UsernameMinimumLength)]
    public required string Username { get; set; }

    [Required,
     StringLength(maximumLength: 24,
         MinimumLength = 3)]
    public required string Password { get; set; }

    [Required,
     StringLength(maximumLength: DeviceTokenValidator.DeviceTokenMaximumLength,
         MinimumLength = DeviceTokenValidator.DeviceTokenMinimumLength)]
    public required string DeviceToken { get; set; }
}