using AppCommon.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Persistence;

public abstract class Repository<TAggregateRoot>
    : IRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    protected DbContext DbContext { get; }

    public Repository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddAsync(TAggregateRoot aggregate, CancellationToken ct = default)
    {
        await DbContext.Set<TAggregateRoot>().AddAsync(aggregate, ct);
    }

    public void Remove(TAggregateRoot aggregate)
    {
        DbContext.Set<TAggregateRoot>().Remove(aggregate);
    }

    public virtual Task<TAggregateRoot?> FindByIdAsync(AggregateRoot.ID id, CancellationToken ct = default)
    {
        return DbContext.Set<TAggregateRoot>().SingleOrDefaultAsync(item => item.Id == id, ct);
    }
}
