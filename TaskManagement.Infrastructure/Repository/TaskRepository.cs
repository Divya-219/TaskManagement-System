using Microsoft.EntityFrameworkCore;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Abstractions.Persistence;

namespace TaskManagement.Infrastructure.Repository;

public class TaskRepository: ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

   
    public async Task AddAsync(TaskItem task)
    { await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task <TaskItem> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id) ?? throw new KeyNotFoundException($"Task with id {id} not found.");
    }
    public async Task UpdateAsync(TaskItem task)
    { _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) { 
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<TaskItem>> GetAllTaskAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

}
