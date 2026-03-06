using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions.Persistence;

public interface ITaskRepository
{
    Task<TaskItem> GetByIdAsync(int id);
    Task<List<TaskItem>> GetAllTaskAsync();
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);

}
