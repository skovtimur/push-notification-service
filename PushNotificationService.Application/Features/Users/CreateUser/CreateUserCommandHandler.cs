using AutoMapper;
using MediatR;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Shared.Domain.Entities;
using PushNotificationService.Shared.Exceptions;

namespace PushNotificationService.Application.Features.Users.CreateUser;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var isExisting = await userRepository.Exists(request.Username);

            if (isExisting)
                throw new BadRequestException($"The Username({request.Username}) has been already taken");

            var user = mapper.Map<UserEntity>(request);
            await userRepository.CreateNewUser(user);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return user.Id;
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}