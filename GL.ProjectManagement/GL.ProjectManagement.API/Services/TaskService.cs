using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.Enums;
using GL.ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GL.ProjectManagement.API.Services
{
    public class TaskService : ITaskService
    {        
        private readonly ITaskRepository repository;

        public TaskService(ITaskRepository repository)
        {            
            this.repository = repository;
        }

        public async System.Threading.Tasks.Task<List<TaskInfo>> GetAllTasks()
        {
            var Tasks = await repository.GetAllTasksAsync();
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var result = new List<TaskInfo>();
                foreach (var task in Tasks)
                {
                    result.Add(new TaskInfo {                        
                        AssignedToUserId = task.AssignedToUserId.ToString(), 
                        Detail = task.Detail, 
                        ProjectId = task.ProjectId.ToString(),
                        Status = (int)task.Status, 
                        CreatedOn = task.CreatedOn.ToString(),
                        Id = task.Id.ToString()
                    });
                }
                return result;
            });
        }

        public async System.Threading.Tasks.Task<TaskInfo> GetTask(string id)
        {
            var Tasks = await repository.GetAllTasksAsync();
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var task = Tasks.FirstOrDefault(p => p.Id == int.Parse(id));
                if (task == null)
                {
                    return null;
                }
                return new TaskInfo { 
                    Id = task.Id.ToString(), 
                    AssignedToUserId = task.AssignedToUserId.ToString(), 
                    Detail = task.Detail, 
                    ProjectId = task.ProjectId.ToString(), 
                    Status = (int)task.Status, 
                    CreatedOn = task.CreatedOn.ToString() };
            });
        }
        public async System.Threading.Tasks.Task<string> CreateTask(TaskCreation newtask)
        {            
            var task = new Task { 
                
                AssignedToUserId = int.Parse(newtask.AssignedToUserId), 
                Detail = newtask.Detail, 
                ProjectId = int.Parse(newtask.ProjectId), 
                Status = Status.New, 
                CreatedOn = DateTime.Now };

            await System.Threading.Tasks.Task.Run(() =>
            {
                repository.CreateTaskAsync(task);
            });
            return $"Task created successfully with id:{task.Id}";
        }

        public async System.Threading.Tasks.Task<bool> DeleteTask(string id)
        {
            var TaskToRemove = await repository.GetTaskByIdAsync(int.Parse(id));
            return await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    repository.DeleteTaskAsync(TaskToRemove);
                    
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async System.Threading.Tasks.Task<string> UpdateTask(TaskUpdate UpdatedTask)
        {
            var task = await repository.GetTaskByIdAsync(int.Parse(UpdatedTask.Id));


            if (task != null)
            {
                task.Detail = UpdatedTask.Detail;
                task.AssignedToUserId = int.Parse(UpdatedTask.AssignedToUserId);
                task.ProjectId = int.Parse(UpdatedTask.ProjectId);
                task.Status = UpdatedTask.Status;
                return await repository.UpdateTaskAsync(task);
            }
            else
            {
                return null;
            }        
        }
    }
}
