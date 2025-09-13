using Microsoft.EntityFrameworkCore.Storage;
using PushNotificationService.Application.Abstraction.RepositoryInterfaces;

namespace PushNotificationService.Infrastructure.Repositories;

public class UnitOfWorkRepository(MainDbContext context) : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(ct);
    }

    public Task CommitAsync(CancellationToken ct = default) =>
        _transaction != null ? _transaction.CommitAsync(ct) : Task.CompletedTask;

    public Task RollbackAsync(CancellationToken ct = default) =>
        _transaction != null ? _transaction.RollbackAsync(ct) : Task.CompletedTask;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await context.SaveChangesAsync(ct);

    public void Dispose()
    {
        _transaction?.Dispose();
        context.Dispose();

        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}