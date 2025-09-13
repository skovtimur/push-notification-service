namespace PushNotificationService.Shared.Domain.ValueObjects;

public class DeviceTokenValueObject
{
    public required string DeviceToken { get; init; }

    public static DeviceTokenValueObject Create(string deviceToken)
    {
        var token = new DeviceTokenValueObject { DeviceToken = deviceToken };
        return DeviceTokenValidator.ThrowIfInvalid(token);
    }
}