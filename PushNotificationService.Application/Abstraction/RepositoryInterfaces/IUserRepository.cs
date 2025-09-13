using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Application.Abstraction.RepositoryInterfaces;

public interface IUserRepository
{
    public Task CreateNewUser(UserEntity user);
    public Task<UserEntity?> GetByUsername(string username);
    public Task<bool> Exists(Guid userId);
    public Task<bool> Exists(string username);
}