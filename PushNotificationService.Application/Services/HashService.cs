using PushNotificationService.Application.Abstraction.ServiceInterfaces;

namespace PushNotificationService.Application.Services;

public class HashService : IHashVerify, IHash
{
    public bool Verify(string str, string hassedString)
    {
        return BCrypt.Net.BCrypt.Verify(str, hassedString);
    }

    public string CreateHash(string str)
    {
        return BCrypt.Net.BCrypt.HashPassword(str);
    }
}