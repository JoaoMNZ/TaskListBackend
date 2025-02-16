namespace TaskListAPI.Models;

public class Task
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Deadline { get; set; }
    public long UserId { get; set; }
}