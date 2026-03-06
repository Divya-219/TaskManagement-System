using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Abstractions.Persistence;

namespace TaskManagement.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository repository)
        {             _taskRepository = repository;
        
        }

        public async Task<TaskItem> CreateAsync(string title, string description)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty");
            }
            var task=new TaskItem(title, description);
            await _taskRepository.AddAsync(task);
            return task;
        }

        public async Task AddAsync(TaskItem task)
        { await _taskRepository.AddAsync(task);
        }


        public async Task UpdateTaskAsync(int id, string title, string description)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new Exception("Task not found");

            task.Update(title, description);

            await _taskRepository.UpdateAsync(task);
        }
        public async Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }
       public async Task<List<TaskItem>> GetAllTaskAsync()
        {
            return await _taskRepository.GetAllTaskAsync();
        }
        public async Task MarkTaskCompleted(int id)
        {
            var task =await _taskRepository.GetByIdAsync(id);
            if(task == null)
            {
                throw new ArgumentException("Task not found");
            } 
            task.MarkAsCompleted();
            await _taskRepository.UpdateAsync(task);

        }
    }
}
