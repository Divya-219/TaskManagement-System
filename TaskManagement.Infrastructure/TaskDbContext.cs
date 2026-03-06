using Microsoft.EntityFrameworkCore;
using taskManagement.Domain.Entities;


namespace TaskManagement.Infrastructure;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions <TaskDbContext> options):base(options)
    {

    }
    public DbSet<TaskItem> Tasks { get; set; }
}

