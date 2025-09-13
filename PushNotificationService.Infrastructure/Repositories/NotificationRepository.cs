using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Shared.Domain.Dtos;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Infrastructure.Repositories;

public class NotificationRepository(MainDbContext mainDbContext, ILogger<NotificationRepository> logger, IMapper mapper)
    : INotificationRepository
{
    public async Task CreateNotification(NotificationEntity notification)
    {
        logger.LogInformation("Creating notification");

        await mainDbContext.Notifications.AddAsync(notification);
    }

    public async Task<List<NotificationDtoToView>> GetHistoryByUsername(string username, DateTime? startDate = null,
        DateTime? endDate = null, int? limit = null)
    {
        username = username.ToUpper();
        var query = mainDbContext.Notifications.Where(n => n.UpperUsername == username);

        if (startDate != null)
        {
            query = query.Where(n => n.CreatedAt >= startDate);
        }

        if (endDate != null)
        {
            query = query.Where(n => n.CreatedAt <= endDate);
        }

        if (limit != null)
        {
            query = query.Take(limit.Value);
        }

        var history = await query
            .Select(x => mapper.Map<NotificationDtoToView>(x))
            .ToListAsync();

        return history;
    }
}