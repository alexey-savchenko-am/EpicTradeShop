using AppCommon.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Output;
using System.Data;
using System.Diagnostics.Metrics;

namespace Persistence;

public sealed class UnitOfWork
    : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<TResult>> ExecuteWithinTransaction<TResult>(
        Func<Task<Result<TResult>>> action,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        var strategy = this._dbContext.Database.CreateExecutionStrategy();

        var result = await strategy.ExecuteAsync(async () =>
        {
            using var transaction = this._dbContext.Database.BeginTransaction(isolationLevel);
            try
            {
                var result = await action();

                if (result.IsFailure) transaction.Rollback();
                else transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Result<TResult>.Failure(new Error("ExecuteWithinTransaction", ex.Message));
            }
        });

        return result;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
}
