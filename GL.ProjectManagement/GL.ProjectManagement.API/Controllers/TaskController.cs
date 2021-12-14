using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectMangement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GL.ProjectManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    public class TaskController : CrudControllerBase<Task, IRepository<Task>>
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IRepository<Task> repo;

        public TaskController(ILogger<TaskController> logger,
            IRepository<Task> repo)
            : base(repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        [AllowAnonymous]
        public override System.Threading.Tasks.Task<IEnumerable<Task>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
       

        //[HttpGet]
        //[Route("api/Task/{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    _logger.LogInformation($"Fetching task with id: {id}");
        //    var task = await _taskService.GetTask(id);
        //    if (task == null)
        //    {
        //        return NotFound($"task not found with id {id}");
        //    }
        //    return Ok(task);
        //}

        //[HttpPost]
        //[Route("api/Task")]
        //public async Task<IActionResult> Create(TaskCreation task)
        //{
        //    _logger.LogInformation("creating new task", 
        //        task.ProjectId, task.AssignedToUserId);
        //    return Ok(await _taskService.CreateTask(task));
        //}

        //[HttpPut]
        //[Route("api/Task")]
        //public async Task<IActionResult> Update(TaskUpdate task)
        //{
        //    _logger.LogInformation($"updating task {task.Id}");
        //    if (await _taskService.UpdateTask(task) == null)
        //    {
        //        return NotFound($"Task not found: {task.Id}");
        //    }
        //    return Ok($"Update successful for task with id: {task.Id}");
        //}

        //[HttpDelete]
        //[Route("api/Task/{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    _logger.LogInformation($"Deleting task: {id}");
        //    var isDeleteSuccessful = await _taskService.DeleteTask(id);
        //    if (isDeleteSuccessful == false)
        //    {
        //        return NotFound($"task not found {id}");
        //    }
        //    return Ok($"Task with id: {id} is deleted");
        //}
    }
}
