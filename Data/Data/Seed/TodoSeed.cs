using Core.Enum;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Data.Seed;

public static class TodoSeed
{
    private static Todo CreateRole(int id, string title, DateTime dueDateT, bool isCompleted, Priority priority)
    {
        return new Todo
        {
            Id = id,
            Title = title,
            Description = "",
            DueDateT = dueDateT,
            IsCompleted = isCompleted,
            Priority = priority
        };
    }

    public static void SeedTodos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().HasData(
            CreateRole(1, "Task 1", DateTime.Now.AddDays(2), false, Priority.Medium),
            CreateRole(2, "Task 2", DateTime.Now.AddDays(6), false, Priority.Low),
            CreateRole(3, "Task 3", DateTime.Now.AddDays(4), false, Priority.High)
        );
    }
}