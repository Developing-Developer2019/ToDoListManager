using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }

    public DbSet<Todo> Todo { get; set; }
    public DbSet<User>AppUser { get; set; }
     
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Configure(); // Extension method, if it exists
        modelBuilder.Seed();      // Extension method, if it exists
    }
}