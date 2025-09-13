using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Application.Features.Notifications.CreateNotification;

public class CreateNotificationCommandHandler(
    INotificationRepository notificationRepository,
    IMapper mapper,
    ILogger<CreateNotificationCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateNotificationCommand, Guid>
{
    public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var notification = mapper.Map<NotificationEntity>(request);

            logger.LogInformation("Notification for {username}. \n Title: {title} \n Text: {text}", request.Username,
                request.Title, request.Text ?? string.Empty);

            await notificationRepository.CreateNotification(notification);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return notification.Id;
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}