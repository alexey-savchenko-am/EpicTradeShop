using SharedKernel;

namespace AppCommon.Persistence;

public interface IRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task AddAsync(TAggregateRoot aggregate, CancellationToken ct = default);
    void Remove(TAggregateRoot aggregate);
    Task<TAggregateRoot?> FindByIdAsync(AggregateRoot.ID id, CancellationToken ct = default);
}
