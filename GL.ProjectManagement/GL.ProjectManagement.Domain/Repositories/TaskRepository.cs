using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {

        public TaskRepository(ProjectManagementDBContext projectManagementDBContext)
            : base(projectManagementDBContext)
        {

        }

        public async System.Threading.Tasks.Task<string> CreateTaskAsync(Task task)
        {
            try
            {
                await AddAsync(task);
                return $"User successfully created with id:{task.Id}";
            }
            catch (Exception e)
            {
                return $"User could not be created with id:{task.Id}";
            }
        }

        public async System.Threading.Tasks.Task<bool> DeleteTaskAsync(Task task)
        {
            try
            {
                await DeleteAsync(task);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async System.Threading.Tasks.Task<Task> GetTaskByIdAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async System.Threading.Tasks.Task<string> UpdateTaskAsync(Task task)
        {
            try
            {
                await UpdateAsync(task);
                return $"Task successfully Update with id:{task.Id}";
            }
            catch (Exception e)
            {
                return $"Task could not be Updated with id:{task.Id}";
            }
        }
    }
}
