using Data.Data.Configure;
using Data.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public static class ModelBuilderExtensions
{
    public static void Configure(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureModels();
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedTodos();
    }
}