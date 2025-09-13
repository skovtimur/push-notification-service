using Microsoft.EntityFrameworkCore;
using PushNotificationService.Infrastructure.EntityConfigurations;
using PushNotificationService.Shared;
using PushNotificationService.Shared.Domain.Entities;

namespace PushNotificationService.Infrastructure;

public class MainDbContext(DbContextOptions<MainDbContext> opt) : DbContext(opt)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationEntityConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var b in
                 ChangeTracker.Entries<BaseEntity>())
        {
            switch (b.State)
            {
                case EntityState.Deleted:
                    b.Entity.RemoveEntity();
                    b.State = EntityState.Modified;

                    break;
            }
        }

        return base.SaveChangesAsync(ct);
    }
}