using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Stock.Infrastructure.Data;

public sealed class StockDbContext
    : DbContextWithOutboxMessages
{
    public StockDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
    }
}
