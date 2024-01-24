using SharedKernel.Output;
using System.Data;

namespace AppCommon.Persistence;

public interface ISession
{
    Task<Result<TResult>> ExecuteWithinTransaction<TResult>(
        Func<Task<Result<TResult>>> action,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    Task StoreAsync(CancellationToken ct = default);
}
