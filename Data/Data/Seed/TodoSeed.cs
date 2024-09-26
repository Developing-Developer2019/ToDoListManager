using Core.Enum;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Data.Seed;

public static class TodoSeed
{
    private static Todo CreateTodo(int id, string userId, string title, DateTime dueDateT, bool isCompleted, Priority priority)
    {
        return new Todo
        {
            Id = id,
            UserId = userId,
            Title = title,
            Description = "Some Description",
            DueDateT = dueDateT,
            IsCompleted = isCompleted,
            Priority = priority
        };
    }

    public static void SeedTodos(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().HasData(
            CreateTodo(1, "dff66c34-bd9b-4355-80ba-e7c7cd02d83f", "Task 1", DateTime.Now.AddDays(2), false, Priority.Medium),
            CreateTodo(2, "dff66c34-bd9b-4355-80ba-e7c7cd02d83f", "Task 2", DateTime.Now.AddDays(6), false, Priority.Low),
            CreateTodo(3, "dff66c34-bd9b-4355-80ba-e7c7cd02d83f", "Task 3", DateTime.Now.AddDays(4), false, Priority.High)
        );
    }
}