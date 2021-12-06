using GL.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        System.Threading.Tasks.Task<Task> GetTaskByIdAsync(int id);
        System.Threading.Tasks.Task<List<Task>> GetAllTasksAsync();
        System.Threading.Tasks.Task<string> CreateTaskAsync(Task task);
        System.Threading.Tasks.Task<string> UpdateTaskAsync(Task task);
        System.Threading.Tasks.Task<bool> DeleteTaskAsync(Task task);
    }
}
