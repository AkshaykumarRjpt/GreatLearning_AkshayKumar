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
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    public class ProjectController : CrudControllerBase<Project, IRepository<Project>>
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IRepository<Project> repo;

        public ProjectController(ILogger<ProjectController> logger,
            IRepository<Project> repo)
            : base(repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        [AllowAnonymous]
        public override Task<IEnumerable<Project>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        //    [HttpGet]
        //    [Route("api/Project")]
        //    public async Task<IActionResult> GetAll()
        //    {
        //        _logger.LogInformation("Fetching All Projects");
        //        return Ok(await _projectService.GetAllProjects());
        //    }

        //    [HttpGet]
        //    [Route("api/Project/{id}")]
        //    public async Task<IActionResult> Get(string id)
        //    {
        //        _logger.LogInformation($"Fetching project with id: {id}");
        //        var project = await _projectService.GetProject(id);
        //        if (project == null)
        //        {
        //            return NotFound($"No project found with id {id}");
        //        }
        //        return Ok(project);
        //    }

        //    [HttpPost]
        //    [Route("api/Project")]
        //    public async Task<IActionResult> Create(ProjectCreation project)
        //    {
        //        _logger.LogInformation("creating new project", project.Name);
        //        return Ok(await _projectService.CreateProject(project));
        //    }

        //    [HttpPut]
        //    [Route("api/Project")]
        //    public async Task<IActionResult> Update(ProjectUpdate project)
        //    {
        //        _logger.LogInformation($"Updating project {project.Id}");
        //        if (await _projectService.UpdateProject(project) == null)
        //        {
        //            return NotFound($"Project not found: {project.Id}");
        //        }
        //        return Ok($"Update successful for project with id: {project.Id}");
        //    }

        //    [HttpDelete]
        //    [Route("api/Project/{id}")]
        //    public async Task<IActionResult> Delete(string id)
        //    {
        //        _logger.LogInformation($"Deleting project with id: {id}");
        //        var isDeleteSuccessful = await _projectService.DeleteProject(id);
        //        if (isDeleteSuccessful == false)
        //        {
        //            return NotFound($"Project not found {id}");
        //        }
        //        return Ok($"project with id: {id} is deleted successfully");
        //    }
    }
}
