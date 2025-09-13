using AutoMapper;
using PushNotificationService.Application.Features.Notifications.CreateNotification;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Shared.Domain.Dtos;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.WebApi.Mapping;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<CreateUserCommand, UserEntity>();
        CreateMap<UserEntity, UserDtoToView>()
            .ForMember(dest => dest.DeviceToken, opt => opt.MapFrom(src => src.DeviceToken.DeviceToken));

        CreateMap<NotificationEntity, NotificationDtoToView>();
        CreateMap<CreateNotificationCommand, NotificationEntity>();
    }
}