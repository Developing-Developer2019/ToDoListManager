using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}