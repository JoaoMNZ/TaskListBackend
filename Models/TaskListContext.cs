using Microsoft.EntityFrameworkCore;

namespace TaskListAPI.Models;

public class TaskListContext : DbContext
{
    public TaskListContext(DbContextOptions<TaskListContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
}