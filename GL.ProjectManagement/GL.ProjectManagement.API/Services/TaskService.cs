using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GL.ProjectManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private IList<Task> Tasks;

        public TaskService()
        {
            this.Tasks = new List<Task>();
        }

        public async System.Threading.Tasks.Task<List<TaskInfo>> GetAllTasks()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var result = new List<TaskInfo>();
                foreach (var task in Tasks)
                {
                    result.Add(new TaskInfo {
                        Id = task.Id, 
                        AssignedToUserId = task.AssignedToUserId, 
                        Detail = task.Detail, 
                        ProjectId = task.ProjectId,
                        Status = (int)task.Status, 
                        CreatedOn = task.CreatedOn.ToString() });
                }
                return result;
            });
        }

        public async System.Threading.Tasks.Task<TaskInfo> GetTask(string id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var task = Tasks.FirstOrDefault(p => p.Id == id);
                if (task == null)
                {
                    return null;
                }
                return new TaskInfo { 
                    Id = task.Id, 
                    AssignedToUserId = task.AssignedToUserId, 
                    Detail = task.Detail, 
                    ProjectId = task.ProjectId, 
                    Status = (int)task.Status, 
                    CreatedOn = task.CreatedOn.ToString() };
            });
        }
        public async System.Threading.Tasks.Task<string> CreateTask(TaskCreation newtask)
        {
            var task = new Task { 
                Id = Guid.NewGuid().ToString(),
                AssignedToUserId = newtask.AssignedToUserId, 
                Detail = newtask.Detail, 
                ProjectId = newtask.ProjectId, 
                Status = Status.New, 
                CreatedOn = DateTime.Now };

            await System.Threading.Tasks.Task.Run(() =>
            {
                Tasks.Add(task);
            });
            return $"Task created successfully with id:{task.Id}";
        }

        public async System.Threading.Tasks.Task<bool> DeleteTask(string id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var TaskToRemove = Tasks.Single(x => x.Id == id);
                    Tasks.Remove(TaskToRemove);
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
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var task = Tasks.FirstOrDefault(x => x.Id == UpdatedTask.Id);
                if (task != null)
                {
                    task.Detail = UpdatedTask.Detail;
                    task.AssignedToUserId = UpdatedTask.AssignedToUserId;
                    task.ProjectId = UpdatedTask.ProjectId;
                    task.Status = UpdatedTask.Status;
                    return task.Id;
                }
                else
                {
                    return null;
                }
            });
        }
    }
}
