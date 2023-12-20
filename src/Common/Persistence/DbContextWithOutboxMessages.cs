using Microsoft.EntityFrameworkCore;

namespace Persistence;

public abstract class DbContextWithOutboxMessages
    : DbContext
{
	public DbSet<OutboxMessage> OutboxMessages { get; set; }

	public DbContextWithOutboxMessages(DbContextOptions options)
		: base(options)
	{}
}
