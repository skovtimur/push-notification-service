using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Infrastructure.Repositories;

public class UserRepository(MainDbContext mainDbContext, ILogger<UserRepository> logger) : IUserRepository
{
    public async Task<bool> Exists(Guid userId)
    {
        var isExisting = await mainDbContext.Users.AnyAsync(u => u.Id == userId);
        return isExisting;
    }

    public async Task<bool> Exists(string username)
    {
        username = username.ToUpper();

        var isExisting = await mainDbContext.Users.AnyAsync(u => u.UpperUsername == username);
        return isExisting;
    }

    public async Task CreateNewUser(UserEntity user)
    {
        logger.LogInformation("Creating new user with ID: {id}", user.Id);

        await mainDbContext.Users.AddAsync(user);
    }

    public async Task<UserEntity?> GetByUsername(string username)
    {
        username = username.ToUpper();

        var user = await mainDbContext.Users
            .Include(x => x.Notifications)
            .SingleOrDefaultAsync(x => x.UpperUsername == username);

        return user;
    }
}