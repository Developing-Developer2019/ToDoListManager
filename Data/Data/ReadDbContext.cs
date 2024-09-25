using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class ReadDbContext(DbContextOptions<WriteDbContext> options) : WriteDbContext(options)
{
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}