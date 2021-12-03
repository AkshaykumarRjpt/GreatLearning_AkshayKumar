using GL.ProjectManagement.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Interfaces
{
    interface ITaskService
    {
        public Task<List<TaskInfo>> GetAllTasks();
        public Task<TaskInfo> GetTask(string id);
        public Task<string> CreateTask(TaskCreation newtask);
        public Task<string> UpdateTask(TaskUpdate UpdatedTask);
        public Task<bool> DeleteTask(string id);
        
    }
}
