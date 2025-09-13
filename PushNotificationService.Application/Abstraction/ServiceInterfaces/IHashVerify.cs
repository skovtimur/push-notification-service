namespace PushNotificationService.Application.Abstraction.ServiceInterfaces;

public interface IHashVerify
{
    public bool Verify(string str, string hassedString);
}