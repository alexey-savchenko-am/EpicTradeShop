using SharedKernel.Output;
using System.Data;

namespace AppCommon.Persistence;

public interface IUnitOfWork
{
    Task<Result<TResult>> ExecuteWithinTransaction<TResult>(
        Func<Task<Result<TResult>>> action,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    Task SaveChangesAsync(CancellationToken ct = default);
}
