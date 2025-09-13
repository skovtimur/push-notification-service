FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 1234

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["PushNotificationService.WebApi/PushNotificationService.WebApi.csproj", "PushNotificationService.WebApi/"]
COPY ["PushNotificationService.Application/PushNotificationService.Application.csproj", "PushNotificationService.Application/"]
COPY ["PushNotificationService.Shared/PushNotificationService.Shared.csproj", "PushNotificationService.Shared/"]
COPY ["PushNotificationService.Infrastructure/PushNotificationService.Infrastructure.csproj", "PushNotificationService.Infrastructure/"]
RUN dotnet restore "PushNotificationService.WebApi/PushNotificationService.WebApi.csproj"
COPY . .
WORKDIR "/src/PushNotificationService.WebApi"
RUN dotnet build "PushNotificationService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "PushNotificationService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PushNotificationService.WebApi.dll"]
