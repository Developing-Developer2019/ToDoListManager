using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todo { get; set; }
}