using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Data.Seed;

public static class UserSeed
{
    private static User CreateRole(string id, string name)
    {
        return new User
        {
            Id = id,
            Name = name
        };
    }

    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            CreateRole("dff66c34-bd9b-4355-80ba-e7c7cd02d83f", "Example User")
        );
    }
}