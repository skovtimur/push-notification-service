using Microsoft.EntityFrameworkCore;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;
using PushNotificationService.Application.Abstraction.ServiceInterfaces;
using PushNotificationService.Application.Features.Users.CreateUser;
using PushNotificationService.Application.Services;
using PushNotificationService.Infrastructure;
using PushNotificationService.Infrastructure.Repositories;
using PushNotificationService.WebApi.Extensions;
using PushNotificationService.WebApi.Mapping;
using PushNotificationService.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MainDbContext>(optBuilder =>
{
    var connectionStr = builder.Configuration.GetConnectionString("Postgres");

    if (string.IsNullOrWhiteSpace(connectionStr))
        throw new NullReferenceException("Postgres connection string is null");

    optBuilder.UseNpgsql(connectionStr);
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHashVerify, HashService>();
builder.Services.AddSingleton<IHash, HashService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddMediatR(x => { x.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly); });
builder.Services.AddAutoMapper(x => { x.AddProfile<MainProfile>(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseRouting();
app.ApplyMigrations();

app.MapControllers();
app.Run();