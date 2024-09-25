using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace Core.Model;

public class Todo
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDateT { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
}